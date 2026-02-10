using System.ComponentModel.DataAnnotations.Schema;

namespace PetHotelApp.Models.DBObjects
{
    public partial class Animal
    {
        public Animal()
        {
            Reservations = new HashSet<Reservation>();
            RoomAllocations = new HashSet<RoomAllocation>();
        }

        public Guid IdAnimal { get; set; }
        public Guid IdOwner { get; set; }
        public string Name { get; set; } = null!;
        public string? Breed { get; set; }
        public string? Notes { get; set; }
        public string? Photo { get; set; }
        public DateTime DateOfBirth { get; set; }

        [ForeignKey("IdOwner")]
        public Owner Owner { get; set; } = null!;

        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<RoomAllocation> RoomAllocations { get; set; }


    }
}
