using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using TeleSharp.TL;
using TeleSharp.TL.Contacts;
using TeleSharp.TL.Messages;
using TeleSharp.TL.Upload;
using TLArchiver.Entities;
using TLArchiver.Utils;
using TLSharp.Core;
using TLSharp.Core.Network;

namespace TLArchiver.Core
{
    public class TLAArchiver
    {
        private Config m_config;
        private WebProxy m_webProxy;
        private FileSessionStore m_store;
        private TelegramClient m_client;
        private string m_hash;

        public TLAArchiver(Config config)
        {
            m_config = config;
            m_store = new FileSessionStore();
            m_webProxy = CreateWebProxy(config);
            m_client = new TelegramClient(m_config.ApiId, m_config.ApiHash, m_store, "session", ConnectViaHttpProxy);
        }

        static public WebProxy CreateWebProxy(Config config)
        {
            if (String.IsNullOrEmpty(config.HttpProxyHost))
                return null;

            var uriBuilder = new UriBuilder
            {
                Scheme = Uri.UriSchemeHttp,
                Host = config.HttpProxyHost,
                Port = config.HttpProxyPort
            };

            var proxy = new WebProxy
            {
                Address = uriBuilder.Uri,
                Credentials = new NetworkCredential(config.ProxyUserName, config.ProxyPassword)
            };

            return proxy;
        }

        public TcpClient ConnectViaHttpProxy(string targetHost, int targetPort)
        {
            if (m_webProxy == null)
                return null;

            var uriBuilder = new UriBuilder
            {
                Scheme = Uri.UriSchemeHttp,
                Host = targetHost,
                Port = targetPort
            };

            var request = WebRequest.Create(uriBuilder.Uri);

            request.Proxy = m_webProxy;
            request.Method = "CONNECT";

            var response = request.GetResponse();

            var responseStream = response.GetResponseStream();
            Debug.Assert(responseStream != null);

            const BindingFlags Flags = BindingFlags.NonPublic | BindingFlags.Instance;

            var rsType = responseStream.GetType();
            var connectionProperty = rsType.GetProperty("Connection", Flags);

            var connection = connectionProperty.GetValue(responseStream, null);
            var connectionType = connection.GetType();
            var networkStreamProperty = connectionType.GetProperty("NetworkStream", Flags);

            var networkStream = networkStreamProperty.GetValue(connection, null);
            var nsType = networkStream.GetType();
            var socketProperty = nsType.GetProperty("Socket", Flags);
            var socket = (Socket)socketProperty.GetValue(networkStream, null);

            return new TcpClient { Client = socket };
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
            TLAbsMessages messages;
            while (iRead < dialog.Total)
            {
                messages = null;
                try
                {
                    messages = (TLAbsMessages)AsyncHelpers.RunSync<TLAbsMessages>(() => m_client.GetHistoryAsync(peer, iRead, 0, m_config.MessagesReadLimit));
                }
                catch (AggregateException e)
                {
                    if (e.InnerException.GetType() == typeof(FloodException))
                    {
                        FloodException f = (FloodException)e.InnerException;
                        Thread.Sleep((int)f.TimeToWait.TotalMilliseconds);
                    }
                    else
                        throw e;
                }

                if (messages != null)
                {
                    TLVector<TLAbsMessage> absMessages = null;

                    if (messages is TLMessagesSlice) // Need several loops to read all the messages
                        absMessages = ((TLMessagesSlice)messages).messages;

                    else if (messages is TLMessages) // All messges had been read at the first loop
                        absMessages = ((TLMessages)messages).messages;

                    else if (messages is TLChannelMessages) // Messages from Channel
                        absMessages = ((TLChannelMessages)messages).messages;

                    else
                        throw new TLCoreException("The message is not a TLMessagesSlice, a TLMessages or a TLChannelMessages");

                    foreach (TLAbsMessage message in absMessages.lists)
                        yield return message;

                    iRead += m_config.MessagesReadLimit;
                }
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
