using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace PetHotelApp.Models
{
    public class AnimalModel
    {
        [Key]
        public Guid IdAnimal { get; set; }

        
        public Guid IdOwner { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "String too long (max. 100 chars)")]
        public string Name { get; set; } = null!;

        [StringLength(100, ErrorMessage = "String too long (max. 100 chars)")]
        public string? Breed { get; set; }

        [StringLength(500, ErrorMessage = "String too long (max. 500 chars)")]
        public string? Notes { get; set; }

        [StringLength(255)]
        public string? Photo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [BindNever]
        public OwnerModel? Owner { get; set; }
    }
}
