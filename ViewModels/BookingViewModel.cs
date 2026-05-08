using System.ComponentModel.DataAnnotations;

namespace TrainTicketManagement.ViewModels
{
    public class BookingViewModel
    {
        public int TrainId { get; set; }
        public List<PassengerInputModel> Passengers { get; set; } = new();
    }

    public class PassengerInputModel
    {
        [Required(ErrorMessage = "Passenger name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be 2-100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Age is required")]
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = string.Empty;
    }
}
