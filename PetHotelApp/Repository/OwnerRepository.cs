using PetHotelApp.Data;
using PetHotelApp.Models;
using PetHotelApp.Models.DBObjects;

namespace PetHotelApp.Repository
{
    public class OwnerRepository
    {
        ApplicationDbContext dbContext;

        public OwnerRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }
        public OwnerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<OwnerModel> GetAllOwners()
        {
            List<OwnerModel> ownerList = new List<OwnerModel>();
            foreach(Owner o in dbContext.Owners)
            {
                ownerList.Add(MapDbObjectToModel(o));
            }
            return ownerList;
        }

        public OwnerModel GetOwnerById(Guid id)
        {
            OwnerModel ownerModel = new OwnerModel();

            ownerModel = MapDbObjectToModel(dbContext.Owners.SingleOrDefault(x => x.IdOwner == id));
            return ownerModel;
        }

        public OwnerModel GetOwnerByPhoneNumber(string phoneNumber)
        {
            OwnerModel ownerModel = new OwnerModel();

            ownerModel = MapDbObjectToModel(dbContext.Owners.SingleOrDefault(x => x.PhoneNumber == phoneNumber));
            return ownerModel;
        }

        public OwnerModel GetOwnerByPhoneEmail(string email)
        {
            OwnerModel ownerModel = new OwnerModel();

            ownerModel = MapDbObjectToModel(dbContext.Owners.SingleOrDefault(x => x.Email == email));
            return ownerModel;
        }

        public OwnerModel GetOwnerByPhoneLastName(string lastName)
        {
            OwnerModel ownerModel = new OwnerModel();

            ownerModel = MapDbObjectToModel(dbContext.Owners.SingleOrDefault(x => x.LastName == lastName));
            return ownerModel;
        }

        public OwnerModel GetOwnerByPhoneFirstName(string firstName)
        {
            OwnerModel ownerModel = new OwnerModel();

            ownerModel = MapDbObjectToModel(dbContext.Owners.SingleOrDefault(x => x.FirstName == firstName));
            return ownerModel;
        }

        public void CreateOwner(OwnerModel ownerModel)
        {
            ownerModel.IdOwner = Guid.NewGuid();
            dbContext.Owners.Add(MapModelToDbObject(ownerModel));
            dbContext.SaveChanges();
        }
        public void UpdateOwner(OwnerModel ownerModel)
        {
            Owner existingOwner = dbContext.Owners.FirstOrDefault(x => x.IdOwner == ownerModel.IdOwner);
            if (existingOwner != null)
            {
                existingOwner.FirstName = ownerModel.FirstName;
                existingOwner.LastName = ownerModel.LastName;
                existingOwner.PhoneNumber = ownerModel.PhoneNumber;
                existingOwner.Email = ownerModel.Email;
                dbContext.SaveChanges();
            }
        }
        public void DeleteOwner(OwnerModel ownerModel)
        {
            Owner existingOwner = dbContext.Owners.FirstOrDefault(x=>x.IdOwner == ownerModel.IdOwner);
            if (existingOwner != null)
            {
                dbContext.Owners.Remove(existingOwner);
                dbContext.SaveChanges();
            }
        }


        private OwnerModel MapDbObjectToModel(Owner dbOwner)
        {
            OwnerModel ownerModel = new OwnerModel();
            if(dbOwner != null)
            {
                ownerModel.IdOwner = dbOwner.IdOwner;
                ownerModel.FirstName = dbOwner.FirstName;
                ownerModel.LastName = dbOwner.LastName;
                ownerModel.PhoneNumber = dbOwner.PhoneNumber;
                ownerModel.Email = dbOwner.Email;
            }

            return ownerModel;
        }
        private Owner MapModelToDbObject(OwnerModel ownerModel)
        {
            Owner owner = new Owner();
            if(ownerModel != null)
            {
                owner.IdOwner = ownerModel.IdOwner;
                owner.FirstName = ownerModel.FirstName;
                owner.LastName = ownerModel.LastName;
                owner.PhoneNumber = ownerModel.PhoneNumber;
                owner.Email = ownerModel.Email;
            }
            return owner;
        }

    }
}
