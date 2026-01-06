using System;
using System.Collections.Generic;

namespace PetHotelApp.Models.DBObjects
{
    public partial class Room
    {
        public Room()
        {
            RoomAllocations = new HashSet<RoomAllocation>();
        }

        public Guid IdRoom { get; set; }
        public int? Capacity { get; set; }
        public decimal? PricePerDay { get; set; }
        public string? RoomType { get; set; }

        public virtual ICollection<RoomAllocation> RoomAllocations { get; set; }
    }
}
