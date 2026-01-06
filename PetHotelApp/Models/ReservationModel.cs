using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Column(TypeName = "nvarchar(20)")]
        public ReservationStatus? Status { get; set; } = ReservationStatus.Pending;
    }
}
