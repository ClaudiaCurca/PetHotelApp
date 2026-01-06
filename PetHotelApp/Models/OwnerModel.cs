using System.ComponentModel.DataAnnotations;

namespace PetHotelApp.Models
{
    public class OwnerModel
    {
        public Guid IdOwner { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
    }
}
