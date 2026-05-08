using TrainTicketManagement.Models;
using TrainTicketManagement.ViewModels;

namespace TrainTicketManagement.Services
{
    public interface IBookingService
    {
        string CreateBooking(BookingViewModel model, string userId);
        Booking? GetBookingByPNR(string pnr);
        Booking? GetBookingById(int id);
        List<Booking> GetBookingsByUser(string userId);  // matches BookingService
        List<Booking> GetAllBookings();
        bool CancelBooking(int bookingId, string userId);
    }
}
