using System;

namespace TheMoonshineCafe.Models
{
    public class Reservation
    {
 
        public int ID {get;  set; }
        public bool PaidInAdvance { get; set; }
        public DateTime TimeResMade { get; set; }
        public Customer Customer { get; set; }
        public int NumberOfSeats { get; set; }
        public Event ResEvent { get; set; }

    }
}