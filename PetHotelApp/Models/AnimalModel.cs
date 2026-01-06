using System.ComponentModel.DataAnnotations;

namespace PetHotelApp.Models
{
    public class AnimalModel
    {
        public Guid IdAnimal {  get; set; }
        public Guid IdOwner { get; set; }
        public string Name { get; set; } = null!;
        public string? Breed { get; set; }
        public string? Notes { get; set; }
        public string? Photo { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
