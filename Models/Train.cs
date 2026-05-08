namespace TrainTicketManagement.Models
{
    public class Train
    {
        public int Id { get; set; }
        public string TrainName { get; set; } = string.Empty;
        public string TrainNumber { get; set; } = string.Empty;
        public int TotalSeats { get; set; }

        // ADDED
        public decimal PricePerSeat { get; set; }

        public int FromStationId { get; set; }
        public Station FromStation { get; set; } = null!;

        public int ToStationId { get; set; }
        public Station ToStation { get; set; } = null!;

        public List<Booking> Bookings { get; set; } = new();
    }
}
