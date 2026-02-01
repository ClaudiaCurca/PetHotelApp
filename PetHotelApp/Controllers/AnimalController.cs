using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;
using PetHotelApp.Data;
using PetHotelApp.Models;
using PetHotelApp.Repository;

namespace PetHotelApp.Controllers
{
    [Authorize]
    public class AnimalController : Controller
    {
        private ReservationRepository _reservationRepository;
        private AnimalRepository _animalRepository;
        private OwnerRepository _ownerRepository;
        public AnimalController(ApplicationDbContext dbContext)
        {
            _reservationRepository = new ReservationRepository(dbContext);
            _animalRepository = new AnimalRepository(dbContext);
            _ownerRepository = new OwnerRepository(dbContext);
        }

        // GET: AnimalController
        public ActionResult Index()
        {
            var animals = _animalRepository.GetAllAnimals();

            if (!User.IsInRole("Admin"))
            {
                var userEmail = User.Identity?.Name;

                var owner = _ownerRepository.GetAllOwners()
                    .FirstOrDefault(o => o.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase));

                if (owner != null)
                {
                    animals = animals.Where(a => a.IdOwner == owner.IdOwner).ToList();
                }
                else
                {
                    animals = new List<AnimalModel>(); 
                }
            }

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
            if (User.IsInRole("Admin"))
            {
                PopulateAnimalsDropdown();
            }
            return View("Create");
        }

        // POST: AnimalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin")]
        public ActionResult Create(ReservationModel model)
        {
            try
            {
                if (!User.IsInRole("Admin"))
                {
                    var animal = _animalRepository.GetAnimalById(model.IdAnimal);
                    if (animal == null)
                    {
                        ModelState.AddModelError("", "Selected animal not found.");
                        PopulateAnimalsDropdown();
                        return View(model);
                    }

                    var owner = _ownerRepository.GetOwnerById(animal.IdOwner);
                    if (owner.Email != User.Identity.Name)
                    {
                        ModelState.AddModelError("", "You can only reserve your own animals.");
                        PopulateAnimalsDropdown();
                        return View(model);
                    }
                }

                if (!ModelState.IsValid)
                {
                    PopulateAnimalsDropdown();
                    return View(model);
                }

                _reservationRepository.CreateReservation(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                PopulateAnimalsDropdown();
                return View(model);
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
        private void PopulateAnimalsDropdown()
        {
            IEnumerable<AnimalModel> animals;

            if (User.IsInRole("Admin"))
            {
                animals = _animalRepository.GetAllAnimals();
            }
            else
            {
                var userEmail = User.Identity?.Name;

                animals = _animalRepository.GetAllAnimals()
                    .Where(a => a.Owner.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase));
            }

            ViewBag.Animals = new SelectList(animals, "IdAnimal", "Name");
        }

    }

}
