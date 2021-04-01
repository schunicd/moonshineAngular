using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheMoonshineCafe.Models
{
    public class Admin
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public int accessLevel { get; set; }
    }
}
