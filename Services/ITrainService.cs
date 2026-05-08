using TrainTicketManagement.Models;

namespace TrainTicketManagement.Services
{
    public interface ITrainService
    {
        List<Train> SearchTrains(string from, string to, DateTime date);
        Train? GetTrainById(int id);
        int GetAvailableSeats(int trainId);
    }
}
