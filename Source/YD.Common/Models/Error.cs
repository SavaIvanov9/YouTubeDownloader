using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YD.Common.Models
{
    public class Error
    {
        public DateTime Date { get; set; } = DateTime.Now;

        public Exception Exception { get; set; }
    }
}