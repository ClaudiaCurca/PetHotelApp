using System.ComponentModel.DataAnnotations;

namespace PetHotelApp.Models
{
    public class ReservationModel
    {
        public Guid IdReservation { get; set; }
        public Guid IdAnimal { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }
    }
}
