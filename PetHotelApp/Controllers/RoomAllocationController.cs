using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using PetHotelApp.Data;
using PetHotelApp.Models;
using PetHotelApp.Repository;

namespace PetHotelApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoomAllocationController : Controller
    {
        private RoomAllocationRepository _repository;
        private RoomRepository _roomRepository;
        private AnimalRepository _animalRepository;

        public RoomAllocationController(ApplicationDbContext dbContext)
        {
            _repository = new RoomAllocationRepository(dbContext);
            _roomRepository = new RoomRepository(dbContext);
            _animalRepository = new AnimalRepository(dbContext);
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
            PopulateAnimalsDropdown();
            PopulateRoomsDropdown();    
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
                PopulateAnimalsDropdown();
                PopulateRoomsDropdown();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                PopulateAnimalsDropdown();
                PopulateRoomsDropdown();
                return View("Create");
            }
        }

        // GET: RoomAllocationController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var allocation = _repository.GetRoomAllocatonById(id);
            return View("Edit", allocation);
        }

        // POST: RoomAllocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var allocation = _repository.GetRoomAllocatonById(id);
                var task = TryUpdateModelAsync(allocation);
                task.Wait();
                if (task.Result)
                {
                    _repository.UpdateRoomAllocation(allocation);
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

        // GET: RoomAllocationController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var allocation = _repository.GetRoomAllocatonById(id);
            return View("Delete",allocation);
        }

        // POST: RoomAllocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var allocation = _repository.GetRoomAllocatonById(id);
                _repository.DeleteAllocation(allocation);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Delete",id);
            }
        }

        private void PopulateAnimalsDropdown()
        {
            var animals = _animalRepository.GetAllAnimals()
                                         .Select(a => new { a.IdAnimal, a.Name, a.DateOfBirth })
                                         .ToList();

            ViewBag.AnimalList = new SelectList(animals, "IdAnimal", "Name", "DateOfBirth");
        }
        private void PopulateRoomsDropdown()
        {
            var animals = _roomRepository.GetAllRooms()
                                         .Select(r => new { r.IdRoom,
                                             DisplayText =
                                            $"Room #{r.IdRoom.ToString().Substring(0, 6)} | " +
                                            $"{r.RoomType} | " +
                                            $"Capacity: {r.Capacity} | " +
                                            $"{r.PricePerDay} ron/day"
                                         })
                                         .ToList();

            ViewBag.RoomList = new SelectList(animals, "IdRoom", "DisplayText");
        }
    }
}
