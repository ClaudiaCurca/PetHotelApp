using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PetHotelApp.Data;
using PetHotelApp.Models;
using PetHotelApp.Repository;

namespace PetHotelApp.Controllers
{
    public class RoomAllocationController : Controller
    {
        private RoomAllocationRepository _repository;

        public RoomAllocationController(ApplicationDbContext dbContext)
        {
            _repository = new RoomAllocationRepository(dbContext);
        }

        // GET: RoomAllocationController
        public ActionResult Index()
        {
            var allocations = _repository.GetAllAllocations();
            return View("Index", allocations);
        }

        // GET: RoomAllocationController/Details/5
        public ActionResult Details(Guid id)
        {
            var allocation = _repository.GetRoomAllocatonById(id);
            return View("Details",allocation);
        }

        // GET: RoomAllocationController/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: RoomAllocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                RoomAllocationModel allocation = new RoomAllocationModel();
                var task = TryUpdateModelAsync(allocation);
                task.Wait();
                if (task.Result)
                {
                    _repository.CreateRoomAllocation(allocation);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: RoomAllocationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoomAllocationController/Edit/5
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

        // GET: RoomAllocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoomAllocationController/Delete/5
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
