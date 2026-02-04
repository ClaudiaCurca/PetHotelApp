using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHotelApp.Data;
using PetHotelApp.Models;
using PetHotelApp.Repository;
using System.Security.Claims;


namespace PetHotelApp.Controllers
{
    [Authorize]
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
            var owners = _repository.GetAllOwners().ToList();

            bool canCreateOwner = true;

            if (User.IsInRole("User"))
            {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                owners = owners
                    .Where(o => o.Email.Equals(userEmail))
                    .ToList();

                canCreateOwner = !owners.Any();
            }

            ViewBag.CanCreateOwner = canCreateOwner;

            return View("Index", owners);
        }

        // GET: OwnerController/Details/5
        public ActionResult Details(Guid id)
        {
            var owner = _repository.GetOwnerById(id);
            return View("Details", owner);
        }

        // GET: OwnerController/Create
        public ActionResult Create()
        {
            if (User.IsInRole("User"))
            {
                var email = User.Identity!.Name;
                var alreadyExists = _repository
                    .GetAllOwners()
                    .Any(o => o.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                if (alreadyExists)
                {
                    return RedirectToAction("Index");
                }
            }

            return View("Create");
        }

        // POST: OwnerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            if (User.IsInRole("User"))
            {
                var email = User.Identity!.Name;
                var alreadyExists = _repository
                    .GetAllOwners()
                    .Any(o => o.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                if (alreadyExists)
                {
                    return RedirectToAction("Index");
                }
            }

            OwnerModel owner = new OwnerModel();
            var task = TryUpdateModelAsync(owner);
            task.Wait();

            if (task.Result)
            {
                _repository.CreateOwner(owner);
            }

            return RedirectToAction(nameof(Index));
        }
    

        // GET: OwnerController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var owner = _repository.GetOwnerById(id);
            return View("Edit",owner);
        }

        // POST: OwnerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var owner = _repository.GetOwnerById(id);
                var task = TryUpdateModelAsync(owner);
                task.Wait();
                if (task.Result)
                {
                    _repository.UpdateOwner(owner);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index",id);
                }
                
            }
            catch
            {
                return RedirectToAction("Index", id);
            }
        }

        // GET: OwnerController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var owner = _repository.GetOwnerById(id);
            if (owner == null)
                return NotFound();

            return View("Delete", owner);
        }

        // POST: OwnerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var owner = _repository.GetOwnerById(id);

                _repository.DeleteOwner(owner);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete", id);
            }
        }
    }
}
