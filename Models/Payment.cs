using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMoonshineCafe.Models
{
    public class Payment
    {
        public int ID { get; set; }
        public double price { get; set; }
        public DateTime paymentDate { get; set; }
        public Customer customer { get; set; }
    }
}
