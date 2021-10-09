using System;

namespace TheMoonshineCafe.Models
{
    public class Reservation
    {
 
        public int id {get;  set; }
        public bool paidInAdvance { get; set; }
        public DateTime timeResMade { get; set; }
        public Customer customerid { get; set; }
        public int numberOfSeats { get; set; }
        public Event resEventid { get; set; }

    }
}