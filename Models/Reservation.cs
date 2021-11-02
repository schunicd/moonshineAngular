using System;

namespace TheMoonshineCafe.Models
{
    public class Reservation
    {
 
        public int id {get;  set; }
        public bool paidInAdvance { get; set; }
        public DateTime timeResMade { get; set; }
        public int customerid { get; set; }
        public int numberOfSeats { get; set; }
        public int resEventid { get; set; }

    }
}