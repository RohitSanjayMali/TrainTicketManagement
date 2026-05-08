using Microsoft.EntityFrameworkCore;
using TrainTicketManagement.Data;
using TrainTicketManagement.Models;

namespace TrainTicketManagement.Services
{
    public class TrainService : ITrainService
    {
        private readonly AppDbContext _context;

        public TrainService(AppDbContext context)
        {
            _context = context;
        }

        public List<Train> SearchTrains(string from, string to, DateTime date)
        {
            return _context.Trains
                .Include(t => t.FromStation)
                .Include(t => t.ToStation)
                .Where(t => t.FromStation.Name == from && t.ToStation.Name == to)
                .ToList();
        }

        public Train? GetTrainById(int id)
        {
            return _context.Trains
                .Include(t => t.FromStation)
                .Include(t => t.ToStation)
                .FirstOrDefault(t => t.Id == id);
        }

        public int GetAvailableSeats(int trainId)
        {
            var train = _context.Trains.FirstOrDefault(t => t.Id == trainId);
            if (train == null) return 0;

            int booked = _context.Bookings
                .Where(b => b.TrainId == trainId)
                .Sum(b => b.TotalPassengers);

            return train.TotalSeats - booked;
        }
    }
}
