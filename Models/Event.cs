using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheMoonshineCafe.Models
{
    public class Event
    {
        public int id {get; set; }
        public DateTime eventStart { get; set; }
        public DateTime eventEnd { get; set; }
        public DateTime refundCutOffDate { get; set; }
        public string bandName { get; set; }
        public string bandImagePath { get; set; }
        public string bandLink { get; set; }
        public int maxNumberOfSeats { get; set; }
        public int currentNumberOfSeats { get; set; }
        public double ticketPrice { get; set; }
        public string description { get; set; }
        public string googleCalID { get; set; }
    }
}
