using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHotelApp.Data;
using PetHotelApp.Models;
using PetHotelApp.Repository;

namespace PetHotelApp.Controllers
{
    public class RoomController : Controller
    {
        private RoomRepository _repository;

        public RoomController(ApplicationDbContext dbContext)
        {
            _repository = new RoomRepository(dbContext);
        }

        // GET: RoomController
        public ActionResult Index()
        {
            var rooms = _repository.GetAllRooms();
            return View("Index", rooms);
        }

        // GET: RoomController/Details/5
        public ActionResult Details(Guid id)
        {
            var room = _repository.GetRoomById(id);
            return View("Details", room);
        }

        // GET: RoomController/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                RoomModel room = new RoomModel();
                var task = TryUpdateModelAsync(room);
                task.Wait();
                if (task.Result)
                {
                    _repository.CreateRoom(room);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: RoomController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoomController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
