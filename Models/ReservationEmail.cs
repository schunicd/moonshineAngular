using System;

namespace moonshineAngular.Models
{
    public class ReservationEmail
    {
        public string subject { get; set; }

        public string body { get; set; }

        public string eventDate { get; set; }

        public string eventName { get; set; }

        public string name { get; set; }
        
        public string email { get; set; }

        public DateTime purchaseDate { get; set; }
  
        public int totalSeats { get; set; }

        public string totalCost { get; set; }

        public string paypalID { get; set; }

    }
}