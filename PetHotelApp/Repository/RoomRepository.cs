using PetHotelApp.Data;
using PetHotelApp.Models;
using PetHotelApp.Models.DBObjects;

namespace PetHotelApp.Repository
{
    public class RoomRepository
    {
        public ApplicationDbContext dbContext;

        public RoomRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }
        public RoomRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<RoomModel> GetAllRooms()
        {
            List<RoomModel> roomList = new List<RoomModel>();
            foreach (Room r in dbContext.Rooms)
            {
                roomList.Add(MapDbObjectToModel(r));
            }
            return roomList;
        }
        public RoomModel GetRoomById(Guid id)
        {
            RoomModel roomModel = MapDbObjectToModel(dbContext.Rooms.FirstOrDefault(x => x.IdRoom == id));
            return roomModel;
        }
        public List<RoomModel> GetAllRoomOrderedAscendingByPrice()
        {
            List<RoomModel> roomList = new List<RoomModel>();
            foreach (Room r in dbContext.Rooms.OrderBy(x => x.PricePerDay))
            {
                roomList.Add(MapDbObjectToModel(r));
            }
            return roomList;
        }

        public void CreateRoom(RoomModel roomModel)
        {
            roomModel.IdRoom = Guid.NewGuid();
            dbContext.Rooms.Add(MapModelToDbObject(roomModel));
            dbContext.SaveChanges();
        }
        public void UpdateRoom(RoomModel roomModel)
        {
            Room existingRoom = dbContext.Rooms.FirstOrDefault(x => x.IdRoom == roomModel.IdRoom);
            if (existingRoom != null)
            {
                existingRoom.Capacity = roomModel.Capacity;
                existingRoom.PricePerDay = roomModel.PricePerDay;
                existingRoom.RoomType = roomModel.RoomType;

                dbContext.SaveChanges();
            }
        }

        public void DeleteRoom(RoomModel roomModel)
        {
            Room existingRooms = dbContext.Rooms.FirstOrDefault(x => x.IdRoom == roomModel.IdRoom);
            if (existingRooms != null)
            {
                dbContext.Rooms.Remove(existingRooms);
                dbContext.SaveChanges();
            }
        }

        private Room MapModelToDbObject(RoomModel roomModel)
        {
            Room room = new Room();
            if (roomModel != null)
            {
                room.IdRoom = roomModel.IdRoom;
                room.PricePerDay = roomModel.PricePerDay;
                room.Capacity = roomModel.Capacity;
                room.RoomType = roomModel.RoomType;
            }
            return room;
        }

        private RoomModel MapDbObjectToModel(Room room)
        {
            RoomModel roomModel = new RoomModel();

            if (room != null)
            {
                roomModel.IdRoom = room.IdRoom;
                roomModel.PricePerDay = room.PricePerDay;
                roomModel.Capacity = room.Capacity;
                roomModel.RoomType = room.RoomType;
            }
            return roomModel;
        }
    }
}
