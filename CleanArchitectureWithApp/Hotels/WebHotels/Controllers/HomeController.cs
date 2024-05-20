using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebHotels.Application.Common.Interfaces;
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
        public IActionResult Index(HomeVM homeVM)
        {
            homeVM.HotelList = _unitOfWork.Hotel.GetAll(includeProperties: "HotelAmenity");
            foreach (var hotel in homeVM.HotelList)
            {
                if (hotel.Id % 2 == 0)
                {
                    hotel.IsAvailable = false;
                }
            }
            return View(homeVM);
        }


        public IActionResult GetVillasByDate(int nights, DateOnly checkInDate)
        {
            var hotelList = _unitOfWork.Hotel.GetAll(includeProperties: "HotelAmenity").ToList();
            foreach (var hotel in hotelList)
            {
                if (hotel.Id % 2 == 0)
                {
                    hotel.IsAvailable = false;
                }
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
