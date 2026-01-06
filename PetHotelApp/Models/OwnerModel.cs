using System.ComponentModel.DataAnnotations;

namespace PetHotelApp.Models
{
    public class OwnerModel
    {
        [Key]
        public Guid IdOwner { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(100, ErrorMessage = "String too long (max. 100 chars)")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(100, ErrorMessage = "String too long (max. 100 chars)")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;
    }
}
