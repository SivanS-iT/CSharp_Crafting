using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebHotels.Application.Common.Interfaces;
using WebHotels.Domain.Entities;
using WebHotels.Infrastructure.Data;

namespace WebHotels.Web.Controllers
{
    public class HotelController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HotelController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var hotels = _unitOfWork.Hotel.GetAll();
            return View(hotels);
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Hotel obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("name", "The description cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {

                if (obj.Image != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\HotelImage");

                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    obj.Image.CopyTo(fileStream);

                    obj.ImageUrl = @"\images\HotelImage\" + fileName;
                }
                else
                {
                    obj.ImageUrl = "https://placehold.co/600x400";
                }


                _unitOfWork.Hotel.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "The hotel has been added successfully.";

                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        public IActionResult Update(int hotelId)
        {
            Hotel? obj = _unitOfWork.Hotel.Get(u => u.Id == hotelId);
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
                _unitOfWork.Hotel.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "The hotel has been updated successfully.";

                return RedirectToAction(nameof(Index));
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
            Hotel? obj = _unitOfWork.Hotel.Get(u => u.Id == hotelId);
            if (obj == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Hotel obj)
        {
            Hotel? objForDelete = _unitOfWork.Hotel.Get(u => u.Id == obj.Id);

            if (objForDelete is not null)
            {
                _unitOfWork.Hotel.Remove(objForDelete);
                _unitOfWork.Save();
                TempData["success"] = "The hotel has been deleted successfully.";

                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Hotel could not be deleted";
            return View();
        }

    }
}
