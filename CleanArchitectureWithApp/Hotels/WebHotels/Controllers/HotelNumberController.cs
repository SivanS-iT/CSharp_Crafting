using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebHotels.Domain.Entities;
using WebHotels.Infrastructure.Data;
using WebHotels.Web.ViewModels;

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
            var hotelsNumbers = _db.HotelNumbers.Include(u => u.Hotel).ToList();
            return View(hotelsNumbers);
        }
        public IActionResult Create()
        {
            HotelNumberVM hotelNumberVM = new()
            {
                HotelList = _db.Hotels.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };

            return View(hotelNumberVM);
        }

        [HttpPost]
        public IActionResult Create(HotelNumberVM obj)
        {
            bool roomNumberExists = _db.HotelNumbers.Any(u => u.Hotel_Number == obj.HotelNumber.Hotel_Number);

            if (ModelState.IsValid && !roomNumberExists)
            {
                _db.HotelNumbers.Add(obj.HotelNumber);
                _db.SaveChanges();
                TempData["success"] = "The villa Number has been created successfully.";
                return RedirectToAction("Index");
            }

            if (roomNumberExists)
            {
                TempData["error"] = "The villa Number already exists.";
            }

            obj.HotelList = _db.Hotels.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(obj);
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
