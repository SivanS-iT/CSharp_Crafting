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
                // selectig just these parameters
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
                return RedirectToAction(nameof(Index));
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


        public IActionResult Update(int hotelNumberId)
        {
            HotelNumberVM hotelNumberVM = new()
            {
                HotelList = _db.Hotels.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                HotelNumber = _db.HotelNumbers.FirstOrDefault(u => u.Hotel_Number == hotelNumberId)
            };

            if (hotelNumberVM.HotelNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(hotelNumberVM);
        }

        [HttpPost]
        public IActionResult Update(HotelNumberVM hotelNumberVM)
        {
            if (ModelState.IsValid)
            {
                _db.HotelNumbers.Update(hotelNumberVM.HotelNumber);
                _db.SaveChanges();
                TempData["success"] = "The villa Number has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            hotelNumberVM.HotelList = _db.Hotels.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            return View(hotelNumberVM);
        }



        public IActionResult Delete(int hotelNumberId)
        {
            HotelNumberVM hotelNumberVM = new()
            {
                HotelList = _db.Hotels.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                HotelNumber = _db.HotelNumbers.FirstOrDefault(u => u.Hotel_Number == hotelNumberId)
            };

            if (hotelNumberVM.HotelNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(hotelNumberVM);
        }

        [HttpPost]
        public IActionResult Delete(HotelNumberVM hotelNumberVM)
        {
            HotelNumber? objForDelete = _db.HotelNumbers.FirstOrDefault(u => u.Hotel_Number == hotelNumberVM.HotelNumber.Hotel_Number);

            if (objForDelete is not null)
            {
                _db.HotelNumbers.Remove(objForDelete);
                _db.SaveChanges();
                TempData["success"] = "The hotelNUmber has been deleted successfully.";

                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Hotel number could not be deleted";
            return View();
        }

    }
}
