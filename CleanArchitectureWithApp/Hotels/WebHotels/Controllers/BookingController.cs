using Microsoft.AspNetCore.Mvc;
using WebHotels.Application.Common.Interfaces;
using WebHotels.Domain.Entities;

namespace WebHotels.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult FinalizeBooking(int hotelId, DateOnly checkInDater, int nights)
            {
            Booking booking = new()
            {
                HotelId = hotelId,
                Hotel = _unitOfWork.Hotel.Get(u => u.Id == hotelId, includeProperties: "HotelAmenity"),
                CheckInDate = checkInDater,
                Nights = nights,
                CheckOutDate = checkInDater.AddDays(nights),
            };

            return View(booking);
        }
    }
}
