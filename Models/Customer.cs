using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TheMoonshineCafe.Models
{
    public class Customer {
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public Boolean onMailingList { get; set; }

    }
}