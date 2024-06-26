using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebHotels.Application.Common.Interfaces;
using WebHotels.Application.Common.Utility;
using WebHotels.Domain.Entities;
using WebHotels.Models;
using WebHotels.Web.ViewModels;

namespace WebHotels.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                HotelList = _unitOfWork.Hotel.GetAll(includeProperties: "HotelAmenity"),
                Nights = 1,
                CheckInDate = DateOnly.FromDateTime(DateTime.Now),
            };  
            return View(homeVM);
        }

        [HttpPost]
        public IActionResult GetHotelsByDate(int nights, DateOnly checkInDate)
        {
            var hotelList = _unitOfWork.Hotel.GetAll(includeProperties: "HotelAmenity").ToList();
            var hotelNumbersList = _unitOfWork.HotelNumber.GetAll().ToList();
            var bookedHotels = _unitOfWork.Booking.GetAll(u => u.Status == SD.StatusApproved ||
            u.Status == SD.StatusCheckedIn).ToList();


            foreach (var hotel in hotelList)
            {
                int roomAvailable = SD.HotelRoomsAvailable_Count(hotel.Id, hotelNumbersList, checkInDate, nights, bookedHotels);

                hotel.IsAvailable = roomAvailable > 0 ? true : false;
            }

            HomeVM homeVM = new()
            {
                CheckInDate = checkInDate,
                HotelList = hotelList,
                Nights = nights
            };

            return PartialView("_hotelList", homeVM);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
