using System;
using System.Collections.Generic;

namespace PetHotelApp.Models.DBObjects
{
    public partial class RoomAllocation
    {
        public Guid IdAllocation { get; set; }
        public Guid IdRoom { get; set; }
        public Guid IdAnimal { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public virtual Animal IdAnimalNavigation { get; set; } = null!;
        public virtual Room IdRoomNavigation { get; set; } = null!;
    }
}
