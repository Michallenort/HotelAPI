﻿using HotelAPI.Entities;
using Microsoft.AspNetCore.Builder;
using NLog.LayoutRenderers.Wrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Users.Any())
                {
                    var users = GetUsers();
                    _dbContext.Users.AddRange(users);
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

        private IEnumerable<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Name = "Jan Kowalski",
                    Email = "kowalski@gmail.com",
                    Nationality = "Polish",
                    DateOfBirth = new DateTime(1990, 2, 14),
                    RoleId = 1,
                    //Role = new Role(),
                    Reservations = new List<Reservation>()
                },
                new User()
                {
                    Name = "Paweł Dobrzyński",
                    Email = "dobrzynski@zf.com",
                    Nationality = "Russian",
                    DateOfBirth = new DateTime(2001, 3, 22),
                    RoleId = 3,
                    //Role = new Role(),
                    Reservations = new List<Reservation>()
                }
            };

            return users;
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Hotel Owner"
                },
                new Role()
                { 
                    Name = "Administrator"
                }
            };

            return roles;
        }
    }
}
