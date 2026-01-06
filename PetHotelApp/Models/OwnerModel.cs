using System.ComponentModel.DataAnnotations;

namespace PetHotelApp.Models
{
    public class OwnerModel
    {
        [Key]
        [Required]
        public Guid IdOwner { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "String too long (max. 100 chars)")]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(100, ErrorMessage = "String too long (max. 100 chars)")]
        public string LastName { get; set; } = null!;

        [Required]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;
    }
}
