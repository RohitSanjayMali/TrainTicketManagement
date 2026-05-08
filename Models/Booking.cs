namespace TrainTicketManagement.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string PNR { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }

        public int TrainId { get; set; }
        public Train Train { get; set; } = null!;

        // ADDED: UserId so each user sees only their own bookings
        public string? UserId { get; set; }

        public int TotalPassengers { get; set; }
        public List<Passenger> Passengers { get; set; } = new();
        public Payment? Payment { get; set; }
    }
}
