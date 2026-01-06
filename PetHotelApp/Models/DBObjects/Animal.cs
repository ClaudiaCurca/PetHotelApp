using System;
using System.Collections.Generic;

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

        public virtual Owner IdOwnerNavigation { get; set; } = null!;
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<RoomAllocation> RoomAllocations { get; set; }
    }
}
