using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetHotelApp.Controllers
{
    public class RoomAllocationController : Controller
    {
        // GET: RoomAllocationController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RoomAllocationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoomAllocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomAllocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
