using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using WebHotels.Domain.Entities;
using WebHotels.Infrastructure.Data;

namespace WebHotels.Web.Controllers
{
    public class HotelNumberController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HotelNumberController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var hotelsNumbers = _db.HotelNumbers.ToList();
            return View(hotelsNumbers);
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> list = _db.Hotels.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            ViewBag.HotelList = list;
            return View();
        }

        [HttpPost]
        public IActionResult Create(HotelNumber obj)
        {
            if (ModelState.IsValid)
            {
                _db.HotelNumbers.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "The hotel number has been added successfully.";

                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult Update(int hotelId)
        {
            Hotel? obj = _db.Hotels.FirstOrDefault(u => u.Id == hotelId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Hotel obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                _db.Hotels.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "The hotel has been updated successfully.";

                return RedirectToAction("Index");
            }
            return View();
        }


        /// <summary>
        /// Delete get endpoint
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public IActionResult Delete(int hotelId)
        {
            Hotel? obj = _db.Hotels.FirstOrDefault(u => u.Id == hotelId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Hotel obj)
        {
            Hotel? objForDelete = _db.Hotels.FirstOrDefault(u => u.Id == obj.Id);

            if (objForDelete is not null)
            {
                _db.Hotels.Remove(objForDelete);
                _db.SaveChanges();
                TempData["success"] = "The hotel has been deleted successfully.";

                return RedirectToAction("Index");
            }
            TempData["error"] = "Hotel could not be deleted";
            return View();
        }

    }
}
