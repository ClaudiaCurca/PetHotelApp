using System.ComponentModel.DataAnnotations;

namespace PetHotelApp.Models
{
    public class RoomAllocationModel
    {
        public Guid IdAllocation { get; set; }
        public Guid IdRoom { get; set; }
        public Guid IdAnimal { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }
    }
}
