using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebHotels.Domain.Entities;



namespace WebHotels.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelNumber> HotelNumbers { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Booking> Bookings{ get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://placehold.co/600x400",
                    Occupancy = 4,
                    Price = 200,
                    Sqft = 550,
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Premium Pool Villa",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://placehold.co/600x401",
                    Occupancy = 4,
                    Price = 300,
                    Sqft = 550,
                },
                new Hotel
                {
                    Id = 3,
                    Name = "Luxury Pool Villa",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://placehold.co/600x402",
                    Occupancy = 4,
                    Price = 400,
                    Sqft = 750,
                });

            modelBuilder.Entity<HotelNumber>().HasData(
                    new HotelNumber
                    {
                        Hotel_Number = 101,
                        HotelId = 1,
                    },
                    new HotelNumber
                    {
                        Hotel_Number = 102,
                        HotelId = 1,
                    },
                    new HotelNumber
                    {
                        Hotel_Number = 103,
                        HotelId = 1,
                    },
                    new HotelNumber
                    {
                        Hotel_Number = 104,
                        HotelId = 1,
                    },
                    new HotelNumber
                    {
                        Hotel_Number = 201,
                        HotelId = 2,
                    },
                    new HotelNumber
                    {
                        Hotel_Number = 202,
                        HotelId = 2,
                    },
                    new HotelNumber
                    {
                        Hotel_Number = 203,
                        HotelId = 2,
                    },
                    new HotelNumber
                    {
                        Hotel_Number = 301,
                        HotelId = 3,
                    },
                    new HotelNumber
                    {
                        Hotel_Number = 302,
                        HotelId = 3,
                    });

            modelBuilder.Entity<Amenity>().HasData(
          new Amenity
          {
              Id = 1,
              HotelId = 1,
              Name = "Private Pool"
          }, new Amenity
          {
              Id = 2,
              HotelId = 1,
              Name = "Microwave"
          }, new Amenity
          {
              Id = 3,
              HotelId = 1,
              Name = "Private Balcony"
          }, new Amenity
          {
              Id = 4,
              HotelId = 1,
              Name = "1 king bed and 1 sofa bed"
          },

          new Amenity
          {
              Id = 5,
              HotelId = 2,
              Name = "Private Plunge Pool"
          }, new Amenity
          {
              Id = 6,
              HotelId = 2,
              Name = "Microwave and Mini Refrigerator"
          }, new Amenity
          {
              Id = 7,
              HotelId = 2,
              Name = "Private Balcony"
          }, new Amenity
          {
              Id = 8,
              HotelId = 2,
              Name = "king bed or 2 double beds"
          },

          new Amenity
          {
              Id = 9,
              HotelId = 3,
              Name = "Private Pool"
          }, new Amenity
          {
              Id = 10,
              HotelId = 3,
              Name = "Jacuzzi"
          }, new Amenity
          {
              Id = 11,
              HotelId = 3,
              Name = "Private Balcony"
          });

        }
    }
}
