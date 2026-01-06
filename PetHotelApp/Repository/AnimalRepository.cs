using PetHotelApp.Data;
using PetHotelApp.Models;
using PetHotelApp.Models.DBObjects;

namespace PetHotelApp.Repository
{
    public class AnimalRepository
    {
        public ApplicationDbContext dbContext;

        public AnimalRepository() 
        { 
            this.dbContext = new ApplicationDbContext();
        }
        public AnimalRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<AnimalModel> GetAllAnimals()
        {
            List<AnimalModel> animalList = new List<AnimalModel>();
            foreach(Animal a in dbContext.Animals)
            {
                animalList.Add(MapDbObjectToModel(a));
            }
            return animalList;
        }

        public AnimalModel GetAnimalById(Guid id)
        {
            AnimalModel animal = new AnimalModel();

            animal = MapDbObjectToModel(dbContext.Animals.SingleOrDefault(x => x.IdAnimal == id));
            return animal;
        }

        public List<AnimalModel> GetAllAnimalsByOwnerId(Guid ownerId)
        {
            List<AnimalModel> animalList = new List<AnimalModel>();

            foreach(Animal a in dbContext.Animals.Where(x=>x.IdOwner== ownerId))
            {

                animalList.Add(MapDbObjectToModel(a));
            }
            
            return animalList;
        }

        public AnimalModel GetAnimalByName(string name)
        {
            AnimalModel animal = new AnimalModel();
            animal = MapDbObjectToModel(dbContext.Animals.FirstOrDefault(x=>x.Name == name));
            return animal;
        }
        public List<AnimalModel> GetAnimalByBreed(string breed)
        {
            List<AnimalModel> animalList = new List<AnimalModel>();
            foreach(Animal a in dbContext.Animals.Where(x=>x.Breed== breed))
            {
                animalList.Add(MapDbObjectToModel(a));
            }
            return animalList;
        }
        // calculate the age of the animal using dateOfBirth and linq
        public List<AnimalModel> GetAllAnimalsByAge (int age)
        {
            List<AnimalModel> animalList = new List<AnimalModel>();
            foreach (Animal a in dbContext.Animals
                .Where(x => 
                    x.DateOfBirth<= DateTime.Today.AddYears(-age) &&
                    x.DateOfBirth > DateTime.Today.AddYears(-(age+1))))
            {
                animalList.Add(MapDbObjectToModel(a));
            }
            return animalList;
        }
        public void CreateAnimal(AnimalModel animalModel)
        {
            animalModel.IdAnimal = Guid.NewGuid();
            dbContext.Animals.Add(MapModelToDbObject(animalModel));
            dbContext.SaveChanges();
        }

        public void Update(AnimalModel animalModel)
        {
            Animal existingAnimal = dbContext.Animals.FirstOrDefault(x=>x.IdAnimal== animalModel.IdAnimal);
            if (existingAnimal != null)
            {
                existingAnimal.IdOwner = animalModel.IdOwner;
                existingAnimal.Name = animalModel.Name;
                existingAnimal.Breed = animalModel.Breed;
                existingAnimal.Notes = animalModel.Notes;
                existingAnimal.Photo = animalModel.Photo;
                existingAnimal.DateOfBirth = animalModel.DateOfBirth;

                dbContext.SaveChanges();
            }
        }

        public void DeleteAnimal(AnimalModel animalModel)
        {
            Animal existingAnimal = dbContext.Animals.FirstOrDefault(x=>x.IdAnimal == animalModel.IdAnimal);
            if (existingAnimal != null)
            {
                dbContext.Animals.Remove(existingAnimal);
                dbContext.SaveChanges();
            }
        }

        private AnimalModel MapDbObjectToModel(Animal dbAnimal)
        {
            AnimalModel animalModel = new AnimalModel();
            if(dbAnimal!= null)
            {
                animalModel.IdAnimal = dbAnimal.IdAnimal;
                animalModel.IdOwner = dbAnimal.IdOwner;
                animalModel.Name = dbAnimal.Name;
                animalModel.Breed = dbAnimal.Breed;
                animalModel.Notes = dbAnimal.Notes;
                animalModel.Photo = dbAnimal.Photo;
                animalModel.DateOfBirth = dbAnimal.DateOfBirth;
                
            }
            return animalModel;

        }
        private Animal MapModelToDbObject(AnimalModel animalModel)
        {
            Animal dbAnimal = new Animal();
            if(animalModel!= null)
            {
                dbAnimal.IdAnimal = animalModel.IdAnimal;
                dbAnimal.IdOwner = animalModel.IdOwner;
                dbAnimal.Name = animalModel.Name;
                dbAnimal.Breed = animalModel.Breed;
                dbAnimal.Notes = animalModel.Notes;
                dbAnimal.Photo = animalModel.Photo;
                dbAnimal.DateOfBirth= animalModel.DateOfBirth;
            }
            return dbAnimal;
        }
    }
}
