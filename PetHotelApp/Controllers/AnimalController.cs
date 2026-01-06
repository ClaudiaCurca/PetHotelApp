using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetHotelApp.Data;
using PetHotelApp.Models;
using PetHotelApp.Repository;

namespace PetHotelApp.Controllers
{
    public class AnimalController : Controller
    {
        private AnimalRepository _repository;
        public AnimalController(ApplicationDbContext dbContext)
        {
            _repository = new AnimalRepository(dbContext);
        }

        // GET: AnimalController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AnimalController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AnimalController/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: AnimalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                AnimalModel model = new AnimalModel();
                var task = TryUpdateModelAsync(model);
                if (task.Result)
                {
                    _repository.CreateAnimal(model);
                }
                return View("Create");
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: AnimalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AnimalController/Edit/5
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

        // GET: AnimalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AnimalController/Delete/5
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
