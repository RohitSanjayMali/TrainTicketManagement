using Microsoft.AspNetCore.Mvc.Rendering;
using TrainTicketManagement.Models;

namespace TrainTicketManagement.ViewModels
{
    public class TrainSearchViewModel
    {
        public string? FromStation { get; set; }
        public string? ToStation { get; set; }
        public DateTime TravelDate { get; set; } = DateTime.Today;

        public List<Train> Results { get; set; } = new();
        public List<SelectListItem> Stations { get; set; } = new();
        public bool SearchPerformed { get; set; }
    }
}
