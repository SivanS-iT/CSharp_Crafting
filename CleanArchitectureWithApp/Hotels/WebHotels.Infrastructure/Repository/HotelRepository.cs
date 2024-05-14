using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebHotels.Application.Common.Interfaces;
using WebHotels.Domain.Entities;

namespace WebHotels.Infrastructure.Repository
{
    internal class HotelRepository : IHotelRepository
    {
        public void Add(Hotel entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hotel> Get(Expression<Func<Hotel, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Hotel> GetAll(Expression<Func<Hotel, bool>>? filter = null, string? includeProperties = null, bool tracked = false)
        {
            throw new NotImplementedException();
        }

        public void Remove(Hotel entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Hotel entity)
        {
            throw new NotImplementedException();
        }
    }
}
