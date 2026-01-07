using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHotelApp.Data;
using PetHotelApp.Models;
using PetHotelApp.Repository;

namespace PetHotelApp.Controllers
{
    public class ReservationController : Controller
    {
        private ReservationRepository _repository;

        public ReservationController(ApplicationDbContext dbContext)
        {
            _repository = new ReservationRepository(dbContext);
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
            return View("Create");
        }

        // POST: ReservationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                ReservationModel reservation = new ReservationModel();
                var task = TryUpdateModelAsync(reservation);
                task.Wait();
                if (task.Result)
                {
                    _repository.CreateReservation(reservation);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Create");
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
    }
}
