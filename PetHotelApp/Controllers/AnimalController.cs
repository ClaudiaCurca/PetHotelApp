using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetHotelApp.Data;
using PetHotelApp.Models;
using PetHotelApp.Repository;

namespace PetHotelApp.Controllers
{
    public class AnimalController : Controller
    {
        private AnimalRepository _animalRepository;
        private OwnerRepository _ownerRepository;
        public AnimalController(ApplicationDbContext dbContext)
        {
            _animalRepository = new AnimalRepository(dbContext);
            _ownerRepository = new OwnerRepository(dbContext);
        }

        // GET: AnimalController
        public ActionResult Index()
        {
            var animals = _animalRepository.GetAllAnimals();
            return View("Index", animals);
        }

        // GET: AnimalController/Details/5
        public ActionResult Details(Guid id)
        {
            var animal = _animalRepository.GetAnimalById(id);
            return View("Details", animal);
        }

        // GET: AnimalController/Create
        public ActionResult Create()
        {
            PopulateOwnersDropdown();
            return View("Create");
        }

        // POST: AnimalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                AnimalModel model = new AnimalModel();

                if (TryUpdateModelAsync(model).Result)
                {
                    _animalRepository.CreateAnimal(model);
                    return RedirectToAction(nameof(Index));
                }

                PopulateOwnersDropdown();
                return View(model);
            }
            catch
            {
                PopulateOwnersDropdown();

                return View();
            }
        }

        // GET: AnimalController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var animal = _animalRepository.GetAnimalById(id);
            return View("Edit",animal);
        }

        // POST: AnimalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = _animalRepository.GetAnimalById(id);
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _animalRepository.Update(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index",id);
                }
                
            }
            catch
            {
                return RedirectToAction("Index",id);
            }
        }

        // GET: AnimalController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var animal = _animalRepository.GetAnimalById(id);
            return View("Delete", animal);
        }

        // POST: AnimalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var animal = _animalRepository.GetAnimalById(id);
                _animalRepository.DeleteAnimal(animal);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete", id);
            }
        }
        private void PopulateOwnersDropdown()
        {
            var owners = _ownerRepository.GetAllOwners()
                                         .Select(o => new { o.IdOwner, FullName = o.FirstName + " " + o.LastName })
                                         .ToList();

            ViewBag.OwnerList = new SelectList(owners, "IdOwner", "FullName");
        }
    }

}
