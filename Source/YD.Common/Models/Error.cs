using System;

namespace YD.Common.Models
{
    public class Error
    {
        public DateTime Date { get; set; } = DateTime.Now;

        public Exception Exception { get; set; }
    }
}