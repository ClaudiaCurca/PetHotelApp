using System;
using System.Collections.Generic;

namespace PetHotelApp.Models.DBObjects
{
    public partial class Reservation
    {
        public Guid IdReservation { get; set; }
        public Guid IdAnimal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ReservationStatus? Status { get; set; } = ReservationStatus.Pending;

        public virtual Animal IdAnimalNavigation { get; set; } = null!;
    }
}
