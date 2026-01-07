using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetHotelApp.Data;
using PetHotelApp.Models;
using PetHotelApp.Repository;

namespace PetHotelApp.Controllers
{
    public class ReservationController : Controller
    {
        private ReservationRepository _repository;
        private AnimalRepository _animalRepository;

        public ReservationController(ApplicationDbContext dbContext)
        {
            _repository = new ReservationRepository(dbContext);
            _animalRepository = new AnimalRepository(dbContext);
        }

        // GET: ReservationController
        public ActionResult Index()
        {
            var reservations = _repository.GetAllReservations();
            return View("Index", reservations);
        }

        // GET: ReservationController/Details/5
        public ActionResult Details(Guid id)
        {
            var reservation = _repository.GetReservationById(id);
            return View("Details", reservation);
        }

        // GET: ReservationController/Create
        public ActionResult Create()
        {
            PopulateAnimalsDropdown();
            return View("Create");
        }

        // POST: ReservationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                ReservationModel model = new ReservationModel();

                if (TryUpdateModelAsync(model).Result)
                {
                    _repository.CreateReservation(model);
                    return RedirectToAction(nameof(Index));
                }

                PopulateAnimalsDropdown();
                return View(model);
            }
            catch
            {
                PopulateAnimalsDropdown();

                return View();
            }
        }

        // GET: ReservationController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var reservation = _repository.GetReservationById(id);
            return View("Edit", reservation);
        }

        // POST: ReservationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var reservation = _repository.GetReservationById(id);
                var task = TryUpdateModelAsync(reservation);
                task.Wait();
                if (task.Result)
                {
                    _repository.UpdateReservation(reservation);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", reservation);
                }

            }
            catch
            {
                return View("Index", id);
            }
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
            var animals = _animalRepository.GetAllAnimals()
                                         .Select(a => new { a.IdAnimal,a.Name,a.DateOfBirth })
                                         .ToList();

            ViewBag.AnimalList = new SelectList(animals, "IdAnimal", "Name","DateOfBirth");
        }
    }
}
