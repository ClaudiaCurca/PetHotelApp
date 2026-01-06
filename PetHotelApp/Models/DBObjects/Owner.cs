using System;
using System.Collections.Generic;

namespace PetHotelApp.Models.DBObjects
{
    public partial class Owner
    {
        public Owner()
        {
            Animals = new HashSet<Animal>();
        }

        public Guid IdOwner { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Animal> Animals { get; set; }
    }
}
