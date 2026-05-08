using Microsoft.EntityFrameworkCore;
using TrainTicketManagement.Data;
using TrainTicketManagement.Helpers;
using TrainTicketManagement.Models;
using TrainTicketManagement.ViewModels;

namespace TrainTicketManagement.Services
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext context)
        {
            _context = context;
        }

        public string CreateBooking(BookingViewModel model, string userId)
        {
            var train = _context.Trains.FirstOrDefault(t => t.Id == model.TrainId)
                ?? throw new InvalidOperationException("Train not found.");

            int bookedSeats    = _context.Bookings.Where(b => b.TrainId == model.TrainId).Sum(b => b.TotalPassengers);
            int requestedSeats = model.Passengers.Count;
            int availableSeats = train.TotalSeats - bookedSeats;

            if (requestedSeats > availableSeats)
                throw new InvalidOperationException($"Only {availableSeats} seat(s) available. You requested {requestedSeats}.");

            var booking = new Booking
            {
                TrainId         = model.TrainId,
                BookingDate     = DateTime.Now,
                PNR             = PnrGenerator.GeneratePNR(),
                UserId          = userId,                       // ADDED: link to user
                TotalPassengers = requestedSeats,
                Passengers      = model.Passengers.Select(p => new Passenger
                {
                    Name   = p.Name,
                    Age    = p.Age,
                    Gender = p.Gender
                }).ToList()
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            var payment = new Payment
            {
                Amount        = requestedSeats * train.PricePerSeat,
                Status        = "Confirmed",
                PaymentMethod = "Online",
                PaidAt        = DateTime.Now,
                BookingId     = booking.Id
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();

            return booking.PNR;
        }

        public Booking? GetBookingByPNR(string pnr)
        {
            return _context.Bookings
                .Include(b => b.Train).ThenInclude(t => t.FromStation)
                .Include(b => b.Train).ThenInclude(t => t.ToStation)
                .Include(b => b.Passengers)
                .Include(b => b.Payment)
                .FirstOrDefault(b => b.PNR == pnr);
        }

        public Booking? GetBookingById(int id)
        {
            return _context.Bookings
                .Include(b => b.Train).ThenInclude(t => t.FromStation)
                .Include(b => b.Train).ThenInclude(t => t.ToStation)
                .Include(b => b.Passengers)
                .Include(b => b.Payment)
                .FirstOrDefault(b => b.Id == id);
        }

        // FIXED: Ab sirf us user ki bookings return hongi
        public List<Booking> GetBookingsByUser(string userId)
        {
            return _context.Bookings
                .Include(b => b.Train).ThenInclude(t => t.FromStation)
                .Include(b => b.Train).ThenInclude(t => t.ToStation)
                .Include(b => b.Payment)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.BookingDate)
                .ToList();
        }

        public List<Booking> GetAllBookings()
        {
            return _context.Bookings
                .Include(b => b.Train).ThenInclude(t => t.FromStation)
                .Include(b => b.Train).ThenInclude(t => t.ToStation)
                .Include(b => b.Payment)
                .OrderByDescending(b => b.BookingDate)
                .ToList();
        }

        public bool CancelBooking(int bookingId, string userId)
        {
            var booking = _context.Bookings
                .Include(b => b.Payment)
                .FirstOrDefault(b => b.Id == bookingId);

            if (booking == null) return false;

            if (booking.Payment != null)
                booking.Payment.Status = "Cancelled";

            _context.SaveChanges();
            return true;
        }
    }
}
