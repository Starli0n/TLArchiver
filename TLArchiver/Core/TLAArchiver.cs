using System;
using System.Collections.Generic;
using TeleSharp.TL;
using TeleSharp.TL.Contacts;
using TeleSharp.TL.Messages;
using TLArchiver.Entities;
using TLArchiver.Utils;
using TLSharp.Core;

namespace TLArchiver.Core
{
    public class TLAArchiver
    {
        private static readonly List<string> c_sExtensions = new List<string>() { "jpg", "gif", "png" };
        private static readonly DateTime c_date0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        private const int c_iInitialIndex = 1;
        private const string c_sPhoto = "Photo";
        private const string c_sVideo = "Video";

        private Config m_config;
        private FileSessionStore m_store;
        private TelegramClient m_client;
        private string m_hash;
        private string m_sPrefix;
        private Dictionary<string, int> m_ExtToIndex;

        public TLAArchiver(Config config)
        {
            m_config = config;
            m_store = new FileSessionStore();
            m_client = new TelegramClient(m_config.ApiId, m_config.ApiHash, m_store, "session");
            m_ExtToIndex = new Dictionary<string, int>();
        }

        public void InitIndex()
        {
            foreach (string ext in c_sExtensions)
                m_ExtToIndex[ext] = c_iInitialIndex;
        }

        public void SendCodeRequest()
        {
             m_hash = AsyncHelpers.RunSync<string>(() => m_client.SendCodeRequestAsync(m_config.NumberToAuthenticate));
        }

        public bool MakeAuth(string code)
        {
            TLUser user = AsyncHelpers.RunSync<TLUser>(() => m_client.MakeAuthAsync(m_config.NumberToAuthenticate, m_hash, code));
            return user != null;
        }

        public bool Connect()
        {
            bool res = AsyncHelpers.RunSync<bool>(() => m_client.ConnectAsync());
            return m_client.IsUserAuthorized();
        }

        public IEnumerable<TLADialog> GetContacts()
        {
            var contacts = AsyncHelpers.RunSync<TLContacts>(() => m_client.GetContactsAsync());
            foreach (var absUser in contacts.users.lists)
            {
                if (absUser.GetType() == typeof(TLUser))
                {
                    var user = (TLUser)absUser;
                    yield return new TLADialog()
                    {
                        Id = user.id,
                        Type = TLADialogType.User,
                        Title = user.first_name + ' ' + user.last_name,
                        Closed = false
                    };
                }
                else
                    throw new TLCoreException("Type of TLDialog is unknown");
            }
        }

        public IEnumerable<TLADialog> GetUserDialogs()
        {
            var dialogs = (TLDialogs)AsyncHelpers.RunSync<TLAbsDialogs>(() => m_client.GetUserDialogsAsync());
            foreach (var absChat in dialogs.chats.lists)
            {
                if (absChat.GetType() == typeof(TLChat))
                {
                    var chat = (TLChat)absChat;
                    yield return new TLADialog()
                    {
                        Id = chat.id,
                        Type = TLADialogType.Chat,
                        Title = chat.title,
                        Date = c_date0.AddSeconds(chat.date).ToLocalTime(),
                        Closed = false
                    };
                }
                else if (absChat.GetType() == typeof(TLChannel))
                {
                    var channel = (TLChannel)absChat;
                    if (channel.access_hash == null)
                        throw new TLAccessHashException();
                    yield return new TLADialog()
                    {
                        Id = channel.id,
                        AccessHash = (long) channel.access_hash,
                        Type = TLADialogType.Channel,
                        Title = channel.title,
                        Date = c_date0.AddSeconds(channel.date).ToLocalTime(),
                        Closed = false
                    };
                }
                else if (absChat.GetType() == typeof(TLChatForbidden))
                {
                    var chatF = (TLChatForbidden)absChat;
                    yield return new TLADialog()
                    {
                        Id = chatF.id,
                        Type = TLADialogType.Chat,
                        Title = chatF.title,
                        Closed = true
                    };
                }
                else if (absChat.GetType() == typeof(TLChannelForbidden))
                {
                    var channelF = (TLChannelForbidden)absChat;
                    yield return new TLADialog()
                    {
                        Id = channelF.id,
                        AccessHash = channelF.access_hash,
                        Type = TLADialogType.Channel,
                        Title = channelF.title,
                        Closed = true
                    };
                }
                else
                    throw new TLCoreException("Type of TLDialog is unknown");
            }
        }

        public TLAbsInputPeer CreatePeerFromDialog(TLADialog dialog)
        {
            TLAbsInputPeer peer = null;
            if (dialog.Type == TLADialogType.Channel)
                peer = new TLInputPeerChannel() { channel_id = dialog.Id, access_hash = dialog.AccessHash };
            else if (dialog.Type == TLADialogType.Chat)
                peer = new TLInputPeerChat() { chat_id = dialog.Id };
            else if (dialog.Type == TLADialogType.User)
                peer = new TLInputPeerUser() { user_id = dialog.Id, access_hash = dialog.AccessHash };
            else
                throw new TLCoreException("Type of TLDialog is unknown");
            return peer;
        }

        public int GetTotalMessages(TLADialog dialog)
        {
            TLAbsInputPeer peer = CreatePeerFromDialog(dialog);

            // Get only one message to have the total amount of messages
            var messages = (TLAbsMessages)AsyncHelpers.RunSync<TLAbsMessages>(() => m_client.GetHistoryAsync(peer, 0, 0, 1));

            if (messages is TLChannelMessages)
                return ((TLChannelMessages)messages).count;
            else if (messages is TLMessagesSlice)
                return ((TLMessagesSlice)messages).count;
            else if (messages is TLMessages)
                return 0; // a user without any talk

            throw new TLCoreException("Unable to find the implementation of TLAbsMessages");
        }

        public IEnumerable<TLAbsMessage> GetMessages(TLADialog dialog)
        {
            TLAbsInputPeer peer = CreatePeerFromDialog(dialog);

            int iRead = 0;
            while (iRead < dialog.Total)
            {
                var messages = (TLAbsMessages)AsyncHelpers.RunSync<TLAbsMessages>(() => m_client.GetHistoryAsync(peer, iRead, 0, m_config.MessagesReadLimit));

                var slice = messages as TLMessagesSlice;
                if (slice == null)
                    throw new TLCoreException("The message is not a TLMessagesSlice");

                foreach (TLAbsMessage message in slice.messages.lists)
                    yield return message;

                iRead += m_config.MessagesReadLimit;
            }
        }

        /*
        public void Export(DateTime dtFrom, DateTime dtTo)
        {
            m_sPrefix = "";

            int? res = AsyncHelpers.RunSync<int?>(() => m_client.ImportContactByPhoneNumber(m_config.NumberToSendMessage));
            if (res == null)
                throw new Exception("ImportContactByPhoneNumber returned a null value");

            List<Message> hist = AsyncHelpers.RunSync<List<Message>>(() => m_client.GetMessagesHistoryForContact(res.Value, 0, 5));

            DateTime date0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime d;
            foreach (MessageConstructor message in hist)
            {
                d = date0.AddSeconds(message.date).ToLocalTime();
                if (d < dtFrom || d > dtTo)
                    continue;
                Prefix = string.Format("{0:0000}-{1:00}-{2:00}", d.Year, d.Month, d.Day);

                if (message.media.GetType() == typeof(MessageMediaPhotoConstructor))
                    ExportPhoto((PhotoConstructor)((MessageMediaPhotoConstructor)message.media).photo);

                else if (message.media.GetType() == typeof(MessageMediaVideoConstructor))
                    ExportVideo();
            }
        }

        private void ExportPhoto(PhotoConstructor photo)
        {
            if (photo.sizes.Count != 3)
                throw new Exception("photo.sizes do not contain 3 elements");

            PhotoSizeConstructor photoSize = (PhotoSizeConstructor)photo.sizes[2];
            FileLocationConstructor f = (FileLocationConstructor)photoSize.location;
            Tuple< storage_FileType, byte[]> img = AsyncHelpers.RunSync<Tuple<storage_FileType, byte[]>>(()
                => m_client.GetFile(f.volume_id, f.local_id, f.secret, 0, photoSize.size + 1024));
            storage_FileType type = img.Item1;
            byte[] bytes = img.Item2;

            string ext = "";
            if (type.GetType() == typeof(Storage_fileJpegConstructor))
                ext = "jpg";
            else if (type.GetType() == typeof(Storage_fileGifConstructor))
                ext = "gif";
            else if (type.GetType() == typeof(Storage_filePngConstructor))
                ext = "png";
            string sFilename = string.Format("{0}-{1}{2:00}.{3}", Prefix, c_sPhoto, GetIndexFromExt(ext), ext);
            using (FileStream file = new FileStream(Path.Combine(ExportDirectory, sFilename), FileMode.Create, FileAccess.Write))
                file.Write(bytes, 4, photoSize.size);
        }
        */

        private void ExportVideo()
        {

        }

        private string Prefix
        {
            get { return m_sPrefix;  }
            set
            {
                if (m_sPrefix != value)
                {
                    InitIndex();
                    m_sPrefix = value;
                }
            }
        }

        private int GetIndexFromExt(string ext)
        {
            if (m_ExtToIndex.ContainsKey(ext))
                return m_ExtToIndex[ext]++;
            else
                return c_iInitialIndex;
        }

        public Config GetConfig()
        {
            return m_config;
        }

        public string ExportDirectory { get; set; }
    }
}
