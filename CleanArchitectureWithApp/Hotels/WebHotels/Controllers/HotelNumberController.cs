using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebHotels.Application.Common.Interfaces;
using WebHotels.Domain.Entities;
using WebHotels.Infrastructure.Data;
using WebHotels.Infrastructure.Repository;
using WebHotels.Web.ViewModels;

namespace WebHotels.Web.Controllers
{
    public class HotelNumberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HotelNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var hotelsNumbers = _unitOfWork.HotelNumber.GetAll(includeProperties: "Hotel");
            return View(hotelsNumbers);
        }
        public IActionResult Create()
        {
            HotelNumberVM hotelNumberVM = new()
            {
                // selectig just these parameters
                HotelList = _unitOfWork.Hotel.GetAll().Select(u => new SelectListItem
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
            bool roomNumberExists = _unitOfWork.HotelNumber.Any(u => u.Hotel_Number == obj.HotelNumber.Hotel_Number);

            if (ModelState.IsValid && !roomNumberExists)
            {
                _unitOfWork.HotelNumber.Add(obj.HotelNumber);
                _unitOfWork.Save();
                TempData["success"] = "The villa Number has been created successfully.";
                return RedirectToAction(nameof(Index));
            }

            if (roomNumberExists)
            {
                TempData["error"] = "The villa Number already exists.";
            }

            obj.HotelList = _unitOfWork.Hotel.GetAll().Select(u => new SelectListItem
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
                HotelList = _unitOfWork.Hotel.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                HotelNumber = _unitOfWork.HotelNumber.Get(u => u.Hotel_Number == hotelNumberId)
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
                _unitOfWork.HotelNumber.Update(hotelNumberVM.HotelNumber);
                _unitOfWork.Save();
                TempData["success"] = "The villa Number has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            hotelNumberVM.HotelList = _unitOfWork.Hotel.GetAll().Select(u => new SelectListItem
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
                HotelList = _unitOfWork.Hotel.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                HotelNumber = _unitOfWork.HotelNumber.Get(u => u.Hotel_Number == hotelNumberId)
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
            HotelNumber? objForDelete = _unitOfWork.HotelNumber.Get(u => u.Hotel_Number == hotelNumberVM.HotelNumber.Hotel_Number);

            if (objForDelete is not null)
            {
                _unitOfWork.HotelNumber.Remove(objForDelete);
                _unitOfWork.Save();
                TempData["success"] = "The hotelNUmber has been deleted successfully.";

                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Hotel number could not be deleted";
            return View();
        }

    }
}
