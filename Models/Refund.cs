using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMoonshineCafe.Models
{
    public class Refund
    {
        public int id { get; set; }
        public Payment payment { get; set; }
        public DateTime refundDate { get; set; }
        public string reason { get; set; }
    }
}
