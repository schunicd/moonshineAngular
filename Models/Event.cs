using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TheMoonshineCafe.Models
{
    public class Event
    {
        
        public int id {get; set; }
        public string title { get; set; }
        public DateTime eventStart { get; set; }
        public DateTime eventEnd { get; set; }
        public int bandId { get; set; }
        public int maxNumberOfSeats { get; set; }
        public int currentNumberOfSeats { get; set; }
        public double ticketPrice { get; set; }
        public string description { get; set; }
        public Band band { get; set; }
        
    }
}
