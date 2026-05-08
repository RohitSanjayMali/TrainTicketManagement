using TrainTicketManagement.Models;

namespace TrainTicketManagement.Services
{
    public interface IPdfService
    {
        byte[] GenerateTicketPdf(Booking booking);
    }
}
