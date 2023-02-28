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
        IEnumerable<ReservationDto> GetAll(int hotelId, int roomId);
        int Create(int hotelId, int roomId, CreateReservationDto dto);
        void Delete(int hotelId, int roomId, int reservationId);
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

        public IEnumerable<ReservationDto> GetAll(int hotelId, int roomId)
        {
            var reservations = _context.Reservations
                .Include(r => r.Room)
                .Include(r => r.User).ToList();

            var reservationsDto = _mapper.Map<List<ReservationDto>>(reservations);

            return reservationsDto;
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

            var user = _context.Users.FirstOrDefault(u => u.Id == dto.UserId);

            if (user is null)
            {
                throw new NotFoundException("User not found");
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

        public void Delete(int hotelId, int roomId, int reservationId)
        {
            var hotel = _context.Hotels.Include(h => h.Rooms).FirstOrDefault(h => h.Id == hotelId);

            if (hotel is null)
            {
                throw new NotFoundException("Hotel not found");
            }

            var room = _context.Rooms.FirstOrDefault(r => r.Id == roomId);

            if (room is null || room.HotelId != hotelId)
            {
                throw new NotFoundException("Room not found");
            }

            var reservation = _context.Reservations.FirstOrDefault(r => r.Id == reservationId);

            if (reservation is null || reservation.RoomId != roomId)
            {
                throw new NotFoundException("Reservation not found");
            }

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }
    }
}
