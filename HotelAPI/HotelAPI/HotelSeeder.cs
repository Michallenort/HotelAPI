using HotelAPI.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI
{
    public class HotelSeeder
    {
        private readonly HotelDbContext _dbContext;

        public HotelSeeder(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Hotels.Any())
                {
                    var hotels = GetHotels();
                    _dbContext.Hotels.AddRange(hotels);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Hotel> GetHotels()
        {
            var hotels = new List<Hotel>()
            {
                new Hotel()
                {
                    Name = "Scout",
                    Description = "Very good and very nice hotel.",
                    Category = "Hotel with swimming pool and fitness",
                    ContactEmail = "scout@gmail.com",
                    ContactNumber = "721512131",
                    Address = new Address()
                    {
                        City = "Częstochowa",
                        Street = "Drogowców 12",
                        PostalCode = "42-200"
                    },
                    Rooms = new List<Room>()
                    {
                        new Room()
                        {
                            Number = 1,
                            Type = "2 members",
                            GuestsNumber = 2,
                            Reservations = new List<Reservation>()
                        },
                        new Room()
                        {
                            Number = 2,
                            Type = "4 members",
                            GuestsNumber = 4,
                            Reservations = new List<Reservation>()
                        },
                    }
                },
                new Hotel()
                {
                    Name = "Grand Hotel",
                    Description = "Hotel with a long tradition",
                    Category = "Hotel with SPA",
                    ContactEmail = "grand@gmail.com",
                    ContactNumber = "7321512131",
                    Address = new Address()
                    {
                        City = "Warszawa",
                        Street = "Pułaskiego 80",
                        PostalCode = "45-200"
                    },
                    Rooms = new List<Room>()
                    {
                        new Room()
                        {
                            Number = 1,
                            Type = "5 members",
                            GuestsNumber = 5,
                            Reservations = new List<Reservation>()
                        },
                        new Room()
                        {
                            Number = 2,
                            Type = "3 members",
                            GuestsNumber = 3,
                            Reservations = new List<Reservation>()
                        },
                    }
                }
            };

            return hotels;
        }
    }
}
