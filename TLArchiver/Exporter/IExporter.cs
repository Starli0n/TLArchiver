using System.Collections.Generic;
using TeleSharp.TL;
using TLArchiver.Entities;

namespace TLArchiver.Exporter
{
    public interface IExporter
    {
        void BeginDialogs(ICollection<TLADialog> dialogs);

        void BeginDialog(TLADialog dialog);

        void BeginMessage(TLAbsMessage absMessage);

        void ExportMessage(TLMessage message);

        void ExportMessageService(TLMessageService message);

        void EndMessage(TLAbsMessage absMessage);

        void EndDialog(TLADialog dialog);

        void EndDialogs(ICollection<TLADialog> m_dialogs);

        void Abort();
    }
}
