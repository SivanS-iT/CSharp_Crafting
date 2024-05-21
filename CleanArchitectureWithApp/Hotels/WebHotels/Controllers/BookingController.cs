using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebHotels.Application.Common.Interfaces;
using WebHotels.Application.Common.Utility;
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

        [Authorize]
        public IActionResult FinalizeBooking(int hotelId, DateOnly checkInDater, int nights)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ApplicationUser user = _unitOfWork.User.Get(u => u.Id == userId);

            Booking booking = new()
            {
                HotelId = hotelId,
                Hotel = _unitOfWork.Hotel.Get(u => u.Id == hotelId, includeProperties: "HotelAmenity"),
                CheckInDate = checkInDater,
                Nights = nights,
                CheckOutDate = checkInDater.AddDays(nights),
                UserId = userId,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Name = user.Name,
            };
            booking.TotalCost = booking.Hotel.Price * nights;
            return View(booking);
        }


        [Authorize]
        [HttpPost]
        public IActionResult FinalizeBooking(Booking booking)
        {

            var hotel = _unitOfWork.Hotel.Get(u => u.Id == booking.HotelId);
            booking.TotalCost = hotel.Price * booking.Nights;

            booking.Status = SD.StatusPending;
            booking.BookingDate = DateTime.Now;

            _unitOfWork.Booking.Add(booking);
            _unitOfWork.Save();
            return RedirectToAction(nameof(BookingConfirmation), new { bookingId = booking.Id });
        }


        [Authorize]
        public IActionResult BookingConfirmation(int bookingId)
        {
            return View(bookingId);
        }
    }
}
