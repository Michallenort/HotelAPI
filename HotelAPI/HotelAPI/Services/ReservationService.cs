using AutoMapper;
using HotelAPI.Entities;
using HotelAPI.Exceptions;
using HotelAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Services
{
    public interface IReservationService
    {
        int Create(int hotelId, int roomId, CreateReservationDto dto);
    }

    public class ReservationService : IReservationService
    {
        private readonly HotelDbContext _context;
        private readonly IMapper _mapper;

        public ReservationService(HotelDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Create(int hotelId, int roomId, CreateReservationDto dto)
        {
            var hotel = _context.Hotels.FirstOrDefault(h => h.Id == hotelId);

            if (hotel is null)
            {
                throw new NotFoundException("Hotel not found");
            }

            var room = _context.Rooms.FirstOrDefault(r => r.Id == roomId);

            if (room is null || room.HotelId != hotelId)
            {
                throw new NotFoundException("Room not found");
            }

            var reservationEntity = _mapper.Map<Reservation>(dto);

            reservationEntity.RoomId = roomId;

            var name = _context.Rooms.Include(r => r.Hotel).FirstOrDefault(r => r.Id == roomId).Hotel.Name;
            var days = (reservationEntity.EndDate - reservationEntity.StartDate).TotalDays;
            var priceForDay = _context.Rooms.FirstOrDefault(r => r.Id == roomId).PriceForNight;

            reservationEntity.HotelName = name;
            reservationEntity.Price = Convert.ToInt32(days) * priceForDay;

            _context.Reservations.Add(reservationEntity);
            _context.SaveChanges();

            return reservationEntity.Id;
        }
    }
}
