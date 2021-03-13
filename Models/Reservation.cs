using System;

namespace TheMoonshineCafe.Models
{
    public class Reservation
    {
 
        public int ID {get;  set; }
        public bool PaidInAdvance { get; set; }
        public DateTime TimeResMade { get; set; }
        public Customer Customer { get; set; }//temp variable stand in for Customer class
        public int NumberOfSeats { get; set; }
        public Event ResEvent { get; set; }//temp variable stand in for Event class

    }
}