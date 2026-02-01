using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        // GET: RoomController/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            var room = _repository.GetRoomById(id);
            return View("Edit",room);
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var room = _repository.GetRoomById(id);
                var task = TryUpdateModelAsync(room);
                task.Wait();
                if (task.Result) 
                {
                    _repository.UpdateRoom(room);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index",id);
                }
                
            }
            catch
            {
                return View("Index",id);
            }
        }

        // GET: RoomController/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            var room = _repository.GetRoomById(id);
            return View("Delete",room);
        }

        // POST: RoomController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var room = _repository.GetRoomById(id);
                _repository.DeleteRoom(room);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete",id);
            }
        }
    }
}
