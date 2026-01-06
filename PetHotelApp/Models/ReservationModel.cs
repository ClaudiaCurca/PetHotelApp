using System.ComponentModel.DataAnnotations;

namespace PetHotelApp.Models
{
    public class ReservationModel
    {
        [Key]
        public Guid IdReservation { get; set; }

        [Required(ErrorMessage = "Animal Id is required")]
        public Guid IdAnimal { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [StringLength(50, ErrorMessage = "String too long (max. 50 chars)")]
        public string? Status { get; set; }
    }
}
