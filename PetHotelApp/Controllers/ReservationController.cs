using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetHotelApp.Data;
using PetHotelApp.Models;
using PetHotelApp.Repository;

namespace PetHotelApp.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private ReservationRepository _repository;
        private AnimalRepository _animalRepository;
        private OwnerRepository _ownerRepository;

        public ReservationController(ApplicationDbContext dbContext)
        {
            _repository = new ReservationRepository(dbContext);
            _animalRepository = new AnimalRepository(dbContext);
            _ownerRepository = new OwnerRepository(dbContext);
        }

        // GET: ReservationController
        public ActionResult Index()
        {
            
            var reservations = _repository.GetAllReservations();

            if (User.IsInRole("User"))
            {
                var userEmail = User.Identity.Name;

                reservations = reservations
                    .Where(r => r.Animal != null
                                && r.Animal.Owner != null
                                && r.Animal.Owner.Email != null
                                && r.Animal.Owner.Email.Equals(userEmail, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View("Index", reservations);
        }

        // GET: ReservationController/Details/5
        public ActionResult Details(Guid id)
        {
            var reservation = _repository.GetReservationById(id);
            return View("Details", reservation);
        }

        // GET: ReservationController/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            PopulateAnimalsDropdown();
            return View("Create");
        }

        // POST: ReservationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationModel model)
        {
            PopulateAnimalsDropdown();

            if (!User.IsInRole("Admin"))
            {
                var animal = _animalRepository.GetAnimalById(model.IdAnimal);
                var owner = _ownerRepository.GetOwnerById(animal.IdOwner);

                if (owner.Email != User.Identity!.Name)
                {
                    ModelState.AddModelError("", "You can only reserve your own animals.");
                    return View(model);
                }
            }

            if (model.IdReservation == Guid.Empty)
                model.IdReservation = Guid.NewGuid();

            _repository.CreateReservation(model);

            return RedirectToAction(nameof(Index));

        }

        // GET: ReservationController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var reservation = _repository.GetReservationById(id);
            ViewBag.StatusList = Enum.GetValues(typeof(ReservationStatus))
                .Cast<ReservationStatus>()
                .ToList();
            return View("Edit", reservation);
        }

        // POST: ReservationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReservationModel model)
        {
            ModelState.Remove("Animal.Name");

            if (!ModelState.IsValid)
            {
                model.Animal = _animalRepository.GetAnimalById(model.IdAnimal);

                ViewBag.StatusList = Enum.GetValues(typeof(ReservationStatus))
                    .Cast<ReservationStatus>()
                    .ToList();

                return View(model);
            }

            if (!User.IsInRole("Admin"))
            {

                var animal = _animalRepository.GetAnimalById(model.IdAnimal);
                var owner = _ownerRepository.GetOwnerById(animal.IdOwner);

                if (owner.Email != User.Identity!.Name)
                {
                    ModelState.AddModelError("", "You cannot edit this reservation.");
                    ViewBag.StatusList = Enum.GetValues(typeof(ReservationStatus)).Cast<ReservationStatus>();
                    return View(model);
                }
            }

            _repository.UpdateReservation(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: ReservationController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var reservation = _repository.GetReservationById(id);
            return View("Delete", reservation);
        }

        // POST: ReservationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var reservation = _repository.GetReservationById(id);
                _repository.DeleteReservation(reservation);
                return RedirectToAction("Index", reservation);
            }
            catch
            {
                return View("Delete", id);
            }
        }

        private void PopulateAnimalsDropdown()
        {
            List<AnimalModel> animals;

            if (User.IsInRole("Admin"))
            {
                animals = _animalRepository.GetAllAnimals();
            }
            else
            {
                var userEmail = User.Identity.Name;

                var owner = _ownerRepository.GetAllOwners()
                                .FirstOrDefault(o => o.Email == userEmail);

                if (owner != null)
                {
                    animals = _animalRepository.GetAllAnimals()
                                .Where(a => a.IdOwner == owner.IdOwner)
                                .ToList();
                }
                else
                {
                    animals = new List<AnimalModel>();
                }
            }

            ViewBag.AnimalList = new SelectList(animals, "IdAnimal", "Name");
        }
    }
}
