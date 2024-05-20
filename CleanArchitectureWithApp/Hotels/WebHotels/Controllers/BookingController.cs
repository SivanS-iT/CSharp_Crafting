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

        public IActionResult FinalizeBooking(int hotelId, DateOnly checkInDate, int nights)
        {
            Booking booking = new()
            {
                HotelId = hotelId,
                Hotel = _unitOfWork.Hotel.Get(u => u.Id == hotelId, includeProperties: "HotelAmenity"),
                CheckInDate = checkInDate,
                Nights = nights,
                CheckOutDate = checkInDate.AddDays(nights),
            };

            return View(booking);
        }
    }
}
