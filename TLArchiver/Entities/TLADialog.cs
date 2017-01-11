using System;

namespace TLArchiver.Entities
{
    public class TLADialog
    {
        public bool Selected { get; set; }
        public int Id { get; set; }
        public TLADialogType Type { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool Closed { get; set; }
        public int Total { get; set; }
    }
}
