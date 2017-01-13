using TeleSharp.TL;

namespace TLArchiver.Exporter
{
    public interface IExporter
    {
        void SetDirectory(string directory);

        void ExportMessage(TLMessage message);

        void ExportMessageService(TLMessageService message);
    }
}
