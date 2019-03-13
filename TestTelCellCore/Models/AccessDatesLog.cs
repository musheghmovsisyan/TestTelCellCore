using System;

namespace TestTelCellCore.Models
{



    public partial class AccessDatesLog
    {
        public int Id { get; set; }
        public int PastId { get; set; }
        public string PastStrId { get; set; }
        public string ActionName { get; set; }
        public DateTime AccessDate { get; set; }=  DateTime.Now;





    }
}