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
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: AnimalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Create(AnimalModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!User.IsInRole("Admin"))
            {
                var owner = _ownerRepository.GetAllOwners()
                             .FirstOrDefault(o => o.Email == User.Identity.Name);
                model.IdOwner = owner.IdOwner;
            }

            _animalRepository.CreateAnimal(model);
            return RedirectToAction("Index");
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

    }

}
