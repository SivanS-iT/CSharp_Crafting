using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebHotels.Domain.Entities;

namespace WebHotels.Application.Common.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        void Update(Booking entity);
        void UpdateStatus(int bookingId, string bookingStatus, int hotelNumber);
        void UpdateStripePaymentID(int bookingId, string sessionId, string paymentIntentId);
    }
}
