using System.ComponentModel.DataAnnotations;

namespace PetHotelApp.Models
{
    public class RoomModel
    {
        [Key]
        public Guid IdRoom { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        public int? Capacity { get; set; }

        public decimal? PricePerDay { get; set; }

        [StringLength(50, ErrorMessage = "Room type too long (max. 50 chars)")]
        public string? RoomType { get; set; }
    }
}
