using System;

namespace moonshineAngular.Models
{
    public class ReservationEmail
    {
        public string subject { get; set; }

        public String eventDate { get; set; }

        public string eventName { get; set; }

        public string name { get; set; }
        
        public string email { get; set; }

        public DateTime purchaseDate { get; set; }

        public String refundCutoff { get; set; }
  
        public int totalSeats { get; set; }

        public string ticketPrice { get; set; }

        public string subtotal { get; set; }

        public string taxes { get; set; }

        public string totalCost { get; set; }

        public string paypalID { get; set; }

    }
}