using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TheMoonshineCafe.Models
{
    public class Event
    {
        
        public int ID {get; set; }
        public DateTime EventDate { get; set; }
        public int BandID { get; set; }
        public int MaxNumberOfSeats { get; set; }
        public int CurrentNumberOfSeats { get; set; }
        public double TicketPrice { get; set; }
        public string Description { get; set; }
        public Band Band { get; set; }
        
    }
}
