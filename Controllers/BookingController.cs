using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrainTicketManagement.Services;
using TrainTicketManagement.ViewModels;

namespace TrainTicketManagement.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IPdfService     _pdfService;

        public BookingController(IBookingService bookingService, IPdfService pdfService)
        {
            _bookingService = bookingService;
            _pdfService     = pdfService;
        }

        [HttpGet]
        public IActionResult Create(int trainId)
        {
            var model = new BookingViewModel
            {
                TrainId    = trainId,
                Passengers = new List<PassengerInputModel> { new() }
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookingViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
                var pnr    = _bookingService.CreateBooking(model, userId);

                TempData["PNR"]            = pnr;
                TempData["SuccessMessage"] = "Booking confirmed!";
                return RedirectToAction("Confirmation");
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(model);
            }
        }

        public IActionResult Confirmation() => View();

        [HttpGet]
        public IActionResult PNRStatus() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PNRStatus(string pnr)
        {
            if (string.IsNullOrWhiteSpace(pnr))
            {
                ModelState.AddModelError(string.Empty, "Please enter a valid PNR.");
                return View();
            }

            var booking = _bookingService.GetBookingByPNR(pnr.Trim().ToUpper());
            if (booking == null)
            {
                TempData["ErrorMessage"] = $"No booking found for PNR: {pnr}";
                return View();
            }

            return View("PNRResult", booking);
        }

        // FIXED: Ab sirf logged-in user ki apni bookings dikhti hain
        public IActionResult History()
        {
            var userId   = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var bookings = _bookingService.GetBookingsByUser(userId);
            return View(bookings);
        }

        // ADDED: Cancel booking action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id)
        {
            var userId  = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
            var success = _bookingService.CancelBooking(id, userId);

            TempData[success ? "SuccessMessage" : "ErrorMessage"] = success
                ? "Booking cancelled successfully."
                : "Could not cancel booking.";

            return RedirectToAction("History");
        }

        public IActionResult DownloadTicket(int id)
        {
            var booking = _bookingService.GetBookingById(id);
            if (booking == null) return NotFound();

            var pdfBytes = _pdfService.GenerateTicketPdf(booking);
            return File(pdfBytes, "application/pdf", $"Ticket_{booking.PNR}.pdf");
        }
    }
}
