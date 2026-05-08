using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrainTicketManagement.ViewModels
{
    public class CreateTrainViewModel
    {
        [Required(ErrorMessage = "Train name is required")]
        public string TrainName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Train number is required")]
        public string TrainNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select source station")]
        public int FromStationId { get; set; }

        [Required(ErrorMessage = "Please select destination station")]
        public int ToStationId { get; set; }

        [Required]
        [Range(1, 5000, ErrorMessage = "Seats must be between 1 and 5000")]
        public int TotalSeats { get; set; }

        [Required]
        [Range(1, 99999, ErrorMessage = "Price must be between 1 and 99999")]
        public decimal PricePerSeat { get; set; }

        public List<SelectListItem> Stations { get; set; } = new();
    }
}
