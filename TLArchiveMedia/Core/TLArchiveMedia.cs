using System;
using System.Collections.Generic;
using System.IO;
using TLSharp.Core;
using TLSharp.Core.MTProto;

namespace TLArchiveMedia
{
    class TLArchiveMedia
    {
        private static readonly List<string> c_sExtensions = new List<string>() { "jpg", "gif", "png" };
        private const int c_iInitialIndex = 1;
        private const string c_sPhoto = "Photo";
        private const string c_sVideo = "Video";

        private Config m_config;
        private FileSessionStore m_store;
        private TelegramClient m_client;
        private string m_hash;
        private string m_sPrefix;
        private Dictionary<string, int> m_ExtToIndex;

        public TLArchiveMedia(Config config)
        {
            m_config = config;
            m_store = new FileSessionStore();
            m_client = new TelegramClient(m_store, "session", m_config.ApiId, m_config.ApiHash);
            m_ExtToIndex = new Dictionary<string, int>();
        }

        public void InitIndex()
        {
            foreach (string ext in c_sExtensions)
                m_ExtToIndex[ext] = c_iInitialIndex;
        }

        public void SendCodeRequest()
        {
             m_hash = AsyncHelpers.RunSync<string>(() => m_client.SendCodeRequest(m_config.NumberToAuthenticate));
        }

        public bool MakeAuth(string code)
        {
            User user = AsyncHelpers.RunSync<User>(() => m_client.MakeAuth(m_config.NumberToAuthenticate, m_hash, code));
            return user != null;
        }

        public bool Connect()
        {
            bool res = AsyncHelpers.RunSync<bool>(() => m_client.Connect());
            return m_client.IsUserAuthorized();
        }

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

        public string ExportDirectory { get; set; }
    }
}
