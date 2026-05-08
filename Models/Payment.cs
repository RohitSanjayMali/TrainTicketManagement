namespace TrainTicketManagement.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal Amount { get; set; }

        // ADDED
        public string PaymentMethod { get; set; } = "Online";
        public DateTime PaidAt { get; set; }

        public int BookingId { get; set; }
        public Booking Booking { get; set; } = null!;
    }
}
