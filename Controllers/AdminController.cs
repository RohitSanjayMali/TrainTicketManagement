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

        // ── DASHBOARD ─────────────────────────────────────────────
        public IActionResult Dashboard()
        {
            ViewBag.TotalTrains = _context.Trains.Count();
            ViewBag.TotalBookings = _context.Bookings.Count();
            ViewBag.TotalUsers = _context.Users.Count();
            ViewBag.TotalRevenue = _context.Payments
                .Where(p => p.Status == "Confirmed")
                .Sum(p => (decimal?)p.Amount) ?? 0;

            return View();
        }

        // ── ALL TRAINS LIST ───────────────────────────────────────
        public IActionResult TrainList()
        {
            var trains = _context.Trains
                .Include(t => t.FromStation)
                .Include(t => t.ToStation)
                .OrderBy(t => t.TrainName)
                .ToList();
            return View(trains);
        }

        // ── CREATE TRAIN ──────────────────────────────────────────
        [HttpGet]
        public IActionResult CreateTrain()
        {
            return View(new CreateTrainViewModel { Stations = GetStations() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTrain(CreateTrainViewModel model)
        {
            if (model.FromStationId == model.ToStationId)
                ModelState.AddModelError("", "Source and destination cannot be the same.");

            if (!ModelState.IsValid)
            {
                model.Stations = GetStations();
                return View(model);
            }

            var train = new Train
            {
                TrainName = model.TrainName,
                TrainNumber = model.TrainNumber,
                TotalSeats = model.TotalSeats,
                PricePerSeat = model.PricePerSeat,
                FromStationId = model.FromStationId,
                ToStationId = model.ToStationId
            };

            _context.Trains.Add(train);
            _context.SaveChanges();

            TempData["SuccessMessage"] = $"Train '{train.TrainName}' added successfully!";
            return RedirectToAction("TrainList");
        }

        // ── EDIT TRAIN ────────────────────────────────────────────
        [HttpGet]
        public IActionResult EditTrain(int id)
        {
            var train = _context.Trains.Find(id);
            if (train == null) return NotFound();

            var model = new CreateTrainViewModel
            {
                Id = train.Id,
                TrainName = train.TrainName,
                TrainNumber = train.TrainNumber,
                TotalSeats = train.TotalSeats,
                PricePerSeat = train.PricePerSeat,
                FromStationId = train.FromStationId,
                ToStationId = train.ToStationId,
                Stations = GetStations()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTrain(CreateTrainViewModel model)
        {
            if (model.FromStationId == model.ToStationId)
                ModelState.AddModelError("", "Source and destination cannot be the same.");

            if (!ModelState.IsValid)
            {
                model.Stations = GetStations();
                return View(model);
            }

            var train = _context.Trains.Find(model.Id);
            if (train == null) return NotFound();

            train.TrainName = model.TrainName;
            train.TrainNumber = model.TrainNumber;
            train.TotalSeats = model.TotalSeats;
            train.PricePerSeat = model.PricePerSeat;
            train.FromStationId = model.FromStationId;
            train.ToStationId = model.ToStationId;

            _context.SaveChanges();

            TempData["SuccessMessage"] = $"Train '{train.TrainName}' updated successfully!";
            return RedirectToAction("TrainList");
        }

        // ── DELETE TRAIN ──────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteTrain(int id)
        {
            var train = _context.Trains.Find(id);
            if (train == null) return NotFound();

            // Safety check: don't delete if active bookings exist
            bool hasActiveBookings = _context.Bookings
                .Any(b => b.TrainId == id);

            if (hasActiveBookings)
            {
                TempData["ErrorMessage"] = $"Cannot delete '{train.TrainName}' — it has existing bookings.";
                return RedirectToAction("TrainList");
            }

            _context.Trains.Remove(train);
            _context.SaveChanges();

            TempData["SuccessMessage"] = $"Train '{train.TrainName}' deleted successfully.";
            return RedirectToAction("TrainList");
        }

        // ── ALL BOOKINGS ──────────────────────────────────────────
        public IActionResult AllBookings()
        {
            var bookings = _context.Bookings
                .Include(b => b.Train)
                .Include(b => b.Payment)
                .OrderByDescending(b => b.BookingDate)
                .ToList();
            return View(bookings);
        }

        // ── HELPER ────────────────────────────────────────────────
        private List<SelectListItem> GetStations() =>
            _context.Stations
                .OrderBy(s => s.Name)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.Name} ({s.Code})"
                }).ToList();
    }
}
