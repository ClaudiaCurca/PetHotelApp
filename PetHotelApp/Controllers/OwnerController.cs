using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHotelApp.Data;
using PetHotelApp.Models;
using PetHotelApp.Repository;

namespace PetHotelApp.Controllers
{
    public class OwnerController : Controller
    {
        private OwnerRepository _repository;

        public OwnerController(ApplicationDbContext dbContext)
        {
            _repository = new OwnerRepository(dbContext);
        }
        // GET: OwnerController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OwnerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OwnerController/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: OwnerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                OwnerModel owner = new OwnerModel();
                var task = TryUpdateModelAsync(owner);
                task.Wait();
                if (task.Result)
                {
                    _repository.CreateOwner(owner);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: OwnerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OwnerController/Edit/5
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

        // GET: OwnerController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OwnerController/Delete/5
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
