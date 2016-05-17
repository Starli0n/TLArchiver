using TLSharp.Core;
using TLSharp.Core.MTProto;

namespace TLArchiveMedia
{
    class TLArchiveMedia
    {
        private Config m_config;
        FileSessionStore m_store;
        TelegramClient m_client;
        string m_hash;        

        public TLArchiveMedia(Config config)
        {
            m_config = config;
            m_store = new FileSessionStore();
            m_client = new TelegramClient(m_store, "session", m_config.ApiId, m_config.ApiHash);
        }

        public void SendCodeRequest()
        {
             m_hash = AsyncHelpers.RunSync<string>(() => m_client.SendCodeRequest(m_config.NumberToAuthenticate));
        }

        public bool MakeAuth(string code)
        {
            var user = AsyncHelpers.RunSync<User>(() => m_client.MakeAuth(m_config.NumberToAuthenticate, m_hash, code));
            return user != null;
        }

        public bool Connect()
        {
            var res = AsyncHelpers.RunSync<bool>(() => m_client.Connect());
            return m_client.IsUserAuthorized();
        }

        public void Export()
        {

        }
    }
}
