using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainTicketManagement.Data;
using TrainTicketManagement.Models;
using TrainTicketManagement.ViewModels;

namespace TrainTicketManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            ViewBag.TotalTrains   = _context.Trains.Count();
            ViewBag.TotalBookings = _context.Bookings.Count();
            ViewBag.TotalUsers    = _context.Users.Count();
            ViewBag.TotalRevenue  = _context.Payments
                .Where(p => p.Status == "Confirmed")
                .Sum(p => (decimal?)p.Amount) ?? 0;

            return View();
        }

        // ADDED: Get form to add new train
        [HttpGet]
        public IActionResult CreateTrain()
        {
            var model = new CreateTrainViewModel
            {
                Stations = _context.Stations
                    .OrderBy(s => s.Name)
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text  = $"{s.Name} ({s.Code})"
                    }).ToList()
            };
            return View(model);
        }

        // ADDED: Submit new train
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTrain(CreateTrainViewModel model)
        {
            if (model.FromStationId == model.ToStationId)
                ModelState.AddModelError("", "Source and destination cannot be the same.");

            if (!ModelState.IsValid)
            {
                model.Stations = _context.Stations
                    .OrderBy(s => s.Name)
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text  = $"{s.Name} ({s.Code})"
                    }).ToList();
                return View(model);
            }

            var train = new Train
            {
                TrainName     = model.TrainName,
                TrainNumber   = model.TrainNumber,
                TotalSeats    = model.TotalSeats,
                PricePerSeat  = model.PricePerSeat,
                FromStationId = model.FromStationId,
                ToStationId   = model.ToStationId
            };

            _context.Trains.Add(train);
            _context.SaveChanges();

            TempData["SuccessMessage"] = $"Train '{train.TrainName}' added successfully!";
            return RedirectToAction("Dashboard");
        }

        // All bookings — Admin only
        public IActionResult AllBookings()
        {
            var bookings = _context.Bookings
                .Include(b => b.Train)
                .Include(b => b.Payment)
                .OrderByDescending(b => b.BookingDate)
                .ToList();
            return View(bookings);
        }
    }
}
