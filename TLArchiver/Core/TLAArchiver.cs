using System.Collections.Generic;
using TeleSharp.TL;
using TeleSharp.TL.Contacts;
using TeleSharp.TL.Messages;
using TeleSharp.TL.Upload;
using TLArchiver.Entities;
using TLArchiver.Utils;
using TLSharp.Core;

namespace TLArchiver.Core
{
    public class TLAArchiver
    {
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
            m_client = new TelegramClient(m_config.ApiId, m_config.ApiHash, m_store, "session",
                m_config.HttpProxyHost, m_config.HttpProxyPort, m_config.ProxyUserName, m_config.ProxyPassword);
            m_ExtToIndex = new Dictionary<string, int>();
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
                        Date = Date.TLConvert(chat.date),
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
                        AccessHash = channel.access_hash.Value,
                        Type = TLADialogType.Channel,
                        Title = channel.title,
                        Date = Date.TLConvert(channel.date),
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

        public TLFile GetFile(TLFileLocation fileLocation, int iSize)
        {
            TLAbsInputFileLocation inputFileLocation = new TLInputFileLocation()
            {
                volume_id = fileLocation.volume_id,
                local_id = fileLocation.local_id,
                secret = fileLocation.secret,
            };
            TLFile file = (TLFile)AsyncHelpers.RunSync<TLFile>(() => m_client.GetFile(inputFileLocation, iSize));
            if (file.bytes.Length != iSize)
                throw new TLCoreException("The file need to be downloaded in parts");
            return file;
        }

        public IEnumerable<TLFile> GetDocument(TLDocument document)
        {
            TLAbsInputFileLocation inputDocument = new TLInputDocumentFileLocation()
            {
                id = document.id,
                access_hash = document.access_hash,
                version = document.version,
            };

            int iRemainingSize = document.size;
            int iOffset = 0;
            int iRead;
            TLFile file;
            while (iRemainingSize > 0)
            {
                file = (TLFile)AsyncHelpers.RunSync<TLFile>(() => m_client.GetFile(inputDocument, iRemainingSize, iOffset));
                iRead = file.bytes.Length;
                iRemainingSize -= iRead;
                iOffset += iRead;
                yield return file;
            }
        }
    }
}
