using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainTicketManagement.Data;
using TrainTicketManagement.Services;
using TrainTicketManagement.ViewModels;

namespace TrainTicketManagement.Controllers
{
    public class TrainController : Controller
    {
        private readonly ITrainService _trainService;
        private readonly AppDbContext _context;

        public TrainController(ITrainService trainService, AppDbContext context)
        {
            _trainService = trainService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Search()
        {
            var model = new TrainSearchViewModel
            {
                Stations = _context.Stations
                    .OrderBy(s => s.Name)
                    .Select(s => new SelectListItem
                    {
                        Value = s.Name,
                        Text = $"{s.Name} ({s.Code})"
                    }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(TrainSearchViewModel model)
        {
            model.Stations = _context.Stations
                .OrderBy(s => s.Name)
                .Select(s => new SelectListItem
                {
                    Value = s.Name,
                    Text = $"{s.Name} ({s.Code})"
                }).ToList();

            if (model.FromStation == model.ToStation)
            {
                ModelState.AddModelError(string.Empty, "Source and destination stations cannot be the same.");
                return View(model);
            }

            model.Results = _trainService.SearchTrains(
                model.FromStation!, model.ToStation!, model.TravelDate);

            model.SearchPerformed = true;

            return View("Results", model);
        }
    }
}
