namespace TrainTicketManagement.Models
{
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // ADDED
        public string Code { get; set; } = string.Empty; // e.g. "NDLS", "CSTM"

        public List<Train> FromTrains { get; set; } = new();
        public List<Train> ToTrains { get; set; } = new();
    }
}