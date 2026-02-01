using Microsoft.EntityFrameworkCore;
using PetHotelApp.Data;
using PetHotelApp.Models;
using PetHotelApp.Models.DBObjects;

namespace PetHotelApp.Repository
{
    public class ReservationRepository
    {
        public ApplicationDbContext dbContext;
        public ReservationRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }
        public ReservationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ReservationModel> GetAllReservations()
        {
            var reservations = dbContext.Reservations
                .Include(r => r.Animal)        
                .ThenInclude(a => a.Owner)     
                .ToList();

            return reservations.Select(r => MapDbObjectToModel(r)).ToList();
        }

        public ReservationModel GetReservationById(Guid id)
        {
            ReservationModel model = MapDbObjectToModel(dbContext.Reservations.FirstOrDefault(x => x.IdReservation == id));
            return model;
        }
        public List<ReservationModel> GetReservationByAnimalId(Guid idAnimal)
        {
            List<ReservationModel> reservationList = new List<ReservationModel>();
            foreach (Reservation r in dbContext.Reservations.Where(x => x.IdAnimal == idAnimal))
            {
                reservationList.Add(MapDbObjectToModel(r));
            }
            return reservationList;
        }
        public List<ReservationModel> GetAllReservationsByStatus(ReservationStatus status)
        {
            List<ReservationModel> reservationList = new List<ReservationModel>();
            foreach (var r in dbContext.Reservations)
            {
               
                if (r.Status == status) 
                {
                    reservationList.Add(MapDbObjectToModel(r));
                }
            }
            return reservationList;
        }

        public List<ReservationModel> GetReservationsByEffectiveDates(DateTime startDate, DateTime endDate)
        {
            List<ReservationModel> reservationList = new List<ReservationModel>();
            foreach (Reservation r in dbContext.Reservations.Where(x => x.StartDate >= startDate && x.EndDate <= endDate))
            {
                reservationList.Add(MapDbObjectToModel(r));
            }
            return reservationList;

        }

        public void CreateReservation(ReservationModel reservationModel)
        {
            reservationModel.IdReservation = Guid.NewGuid();
            dbContext.Reservations.Add(MapModelToDbObject(reservationModel));
            dbContext.SaveChanges();
        }

        public void UpdateReservation(ReservationModel reservationModel)
        {
            Reservation existingReservation = dbContext.Reservations.FirstOrDefault(x => x.IdReservation == reservationModel.IdReservation);
            if (existingReservation != null)
            {
                existingReservation.IdAnimal = reservationModel.IdAnimal;
                existingReservation.StartDate = reservationModel.StartDate;
                existingReservation.EndDate = reservationModel.EndDate;
                existingReservation.Status = reservationModel.Status;
                dbContext.SaveChanges();
            }
        }
        public void DeleteReservation(ReservationModel reservationModel)
        {
            Reservation existingReservation = dbContext.Reservations.FirstOrDefault(x => x.IdReservation == reservationModel.IdReservation);
            if (existingReservation != null)
            {
                dbContext.Reservations.Remove(existingReservation);
                dbContext.SaveChanges();
            }
        }
        private Reservation MapModelToDbObject(ReservationModel reservationModel)
        {
            Reservation reservation = new Reservation();
            if (reservationModel != null)
            {
                reservation.IdReservation = reservationModel.IdReservation;
                reservation.IdAnimal = reservationModel.IdAnimal;
                reservation.StartDate = reservationModel.StartDate;
                reservation.EndDate = reservationModel.EndDate;
                reservation.Status = reservationModel.Status;
            }
            return reservation;
        }

        private ReservationModel MapDbObjectToModel(Reservation dbReservation)
        {
            ReservationModel model = new ReservationModel();
            if (dbReservation != null)
            {
                model.IdReservation = dbReservation.IdReservation;
                model.IdAnimal = dbReservation.IdAnimal;
                model.StartDate = dbReservation.StartDate;
                model.EndDate = dbReservation.EndDate;
                model.Status = dbReservation.Status;
                if (dbReservation.Animal != null)
                {
                    model.Animal = new AnimalModel
                    {
                        IdAnimal = dbReservation.Animal.IdAnimal,
                        Name = dbReservation.Animal.Name,
                        IdOwner = dbReservation.Animal.IdOwner,
                        Breed = dbReservation.Animal.Breed,

                        Owner = new OwnerModel
                        {
                            IdOwner = dbReservation.Animal.Owner.IdOwner,
                            Email = dbReservation.Animal.Owner.Email
                        }
                    };
                }
            }
            return model;
        }
    }
}
