using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebHotels.Domain.Entities;

namespace WebHotels.Application.Common.Utility
{
    public class SD
    {
        public const string Role_Customer = "Customer";
        public const string Role_Admin = "Admin";

        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusCheckedIn = "CheckedIn";
        public const string StatusCompleted = "Completed";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";


        public static int HotelRoomsAvailable_Count(int hotelId, List<HotelNumber> hotelNumberList, DateOnly checkInDate, int nights,List<Booking> bookings)
        {
            List<int> bookingInDate = new();
            int finalAvailableRoomForAllNights = int.MaxValue;

            var roomsInHotel = hotelNumberList.Where(x => x.HotelId == hotelId).Count();

            for (int i = 0; i < nights; i++)
            {
                var hotelsBooked = bookings.Where(u => u.CheckInDate <= checkInDate.AddDays(i)
                && u.CheckOutDate > checkInDate.AddDays(i) && u.HotelId == hotelId);

                foreach (var booking in hotelsBooked)
                {
                    if (!bookingInDate.Contains(booking.Id))
                    {
                        bookingInDate.Add(booking.Id);
                    }
                }

                var totalAvailableRooms = roomsInHotel - bookingInDate.Count;
                if (totalAvailableRooms == 0)
                {
                    return 0;
                }
                else
                {
                    if (finalAvailableRoomForAllNights > totalAvailableRooms)
                    {
                        finalAvailableRoomForAllNights = totalAvailableRooms;
                    }
                }
            }

            return finalAvailableRoomForAllNights;
        }
        

    }
}
