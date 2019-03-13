using System;

namespace TestTelCellCore.Models
{



    public partial class Paste
    {
        public int Id { get; set; }
        public string PasteStrId { get; set; }
        public string PasteText { get; set; }
        public DateTime InputDate { get; set; } = DateTime.Now;



    }


    }