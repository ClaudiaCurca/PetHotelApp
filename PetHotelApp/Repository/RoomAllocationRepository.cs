using PetHotelApp.Data;
using PetHotelApp.Models;
using PetHotelApp.Models.DBObjects;

namespace PetHotelApp.Repository
{
    public class RoomAllocationRepository
    {
        public ApplicationDbContext dbContext;
        public RoomAllocationRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }
        public RoomAllocationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<RoomAllocationModel> GetAllAllocations()
        {
            List<RoomAllocationModel> roomAllocationsList = new List<RoomAllocationModel>();
            
            foreach(RoomAllocation r in dbContext.RoomAllocations)
            {
                roomAllocationsList.Add(MapDbObjectToModel(r));
            }

            return roomAllocationsList;
        }
        public RoomAllocationModel GetRoomAllocatonById(Guid id)
        {
            RoomAllocationModel roomAllocationModel = new RoomAllocationModel();
            roomAllocationModel = MapDbObjectToModel(dbContext.RoomAllocations.FirstOrDefault(x => x.IdAllocation == id));
            return roomAllocationModel;
        
        }
        public List<RoomAllocationModel> GetRoomAllocatonByIdRoom(Guid roomId)
        {
            List<RoomAllocationModel> roomAllocationList = new List<RoomAllocationModel>();
            foreach(RoomAllocation r in dbContext.RoomAllocations.Where(x=>x.IdRoom==roomId))
            {
                roomAllocationList.Add(MapDbObjectToModel(r));
            }
            return roomAllocationList;
        }
        public List<RoomAllocationModel> GetRoomAllocatonByIdAnimal(Guid animalId)
        {
            List<RoomAllocationModel> roomAllocationList = new List<RoomAllocationModel>();
            foreach (RoomAllocation r in dbContext.RoomAllocations.Where(x => x.IdAnimal == animalId))
            {
                roomAllocationList.Add(MapDbObjectToModel(r));
            }
            return roomAllocationList;
        }

        public List<RoomAllocationModel> GetReservationsByEffectiveDates(DateTime startDate, DateTime endDate)
        {
            List<RoomAllocationModel> roomAllocationList = new List<RoomAllocationModel>();
            foreach (RoomAllocation r in dbContext.RoomAllocations.Where(x => x.CheckInDate >= startDate && x.CheckOutDate <= endDate))
            {
                roomAllocationList.Add(MapDbObjectToModel(r));
            }
            return roomAllocationList;

        }
        public void CreateRoomAllocation(RoomAllocationModel roomAllocationModel)
        {
            roomAllocationModel.IdAllocation = Guid.NewGuid();
            dbContext.Add(MapModelToDbObject(roomAllocationModel));
            dbContext.SaveChanges();
        }

        public void UpdateRoomAllocation(RoomAllocationModel roomAllocationModel)
        {
            RoomAllocation existingAllocation = dbContext.RoomAllocations.FirstOrDefault(x => x.IdAllocation == roomAllocationModel.IdAllocation);
            if (existingAllocation != null)
            {
                existingAllocation.IdAnimal = roomAllocationModel.IdAnimal;
                existingAllocation.IdRoom = roomAllocationModel.IdRoom;
                existingAllocation.CheckInDate = roomAllocationModel.CheckInDate;
                existingAllocation.CheckOutDate = roomAllocationModel.CheckOutDate;
                dbContext.SaveChanges();
            }
        }
        public void DeleteAllocation(RoomAllocationModel roomAllocationModel)
        {
            RoomAllocation existingAllocation = dbContext.RoomAllocations.FirstOrDefault(x => x.IdAllocation == roomAllocationModel.IdAllocation);
            if (existingAllocation != null)
            {
                dbContext.RoomAllocations.Remove(existingAllocation);
                dbContext.SaveChanges();
            }
        }

        private RoomAllocation MapModelToDbObject(RoomAllocationModel roomAllocationModel)
        {
            RoomAllocation roomAllocation = new RoomAllocation();
            if(roomAllocationModel != null)
            {
                roomAllocation.IdAllocation = roomAllocationModel.IdAllocation;
                roomAllocation.IdAnimal = roomAllocationModel.IdAnimal;
                roomAllocation.IdRoom = roomAllocationModel.IdRoom;
                roomAllocation.CheckInDate = roomAllocationModel.CheckInDate;
                roomAllocation.CheckOutDate = roomAllocationModel.CheckOutDate; 
            }
            return roomAllocation;
        }

        private RoomAllocationModel MapDbObjectToModel(RoomAllocation roomAllocation)
        {
            RoomAllocationModel roomAllocationModel = new RoomAllocationModel();

            if(roomAllocation != null)
            {
                roomAllocationModel.IdAllocation = roomAllocation.IdAllocation;
                roomAllocationModel.IdAnimal = roomAllocation.IdAnimal;
                roomAllocationModel.IdRoom = roomAllocation.IdRoom;
                roomAllocationModel.CheckInDate = roomAllocation.CheckInDate;
                roomAllocationModel.CheckOutDate = roomAllocation.CheckOutDate;
            }
            return roomAllocationModel;
        }
    }
}
