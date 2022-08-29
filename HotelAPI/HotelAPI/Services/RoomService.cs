using AutoMapper;
using HotelAPI.Entities;
using HotelAPI.Exceptions;
using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;
using NLog.Web.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Services
{
    public interface IRoomService
    {
        int Create(int hotelId, CreateRoomDto dto);
        List<RoomDto> GetAll(int hotelId);
        RoomDto GetById(int hotelId, int roomId);
        void Update(int hotelId, int roomId, UpdateRoomDto dto);
        void DeleteById(int hotelId, int roomId);
        void DeleteAll(int hotelId);
    }

    public class RoomService : IRoomService
    {
        private readonly HotelDbContext _context;
        private readonly IMapper _mapper;

        public RoomService(HotelDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Create(int hotelId, CreateRoomDto dto)
        {
            var hotel = _context.Hotels.FirstOrDefault(h => h.Id == hotelId);

            if (hotel is null)
            {
                throw new NotFoundException("Hotel not found");
            }

            var roomEntity = _mapper.Map<Room>(dto);

            roomEntity.HotelId = hotelId;

            _context.Rooms.Add(roomEntity);
            _context.SaveChanges();

            return roomEntity.Id;

        }

        public List<RoomDto> GetAll(int hotelId)
        {
            var hotel = _context.Hotels.Include(h => h.Rooms).FirstOrDefault(h => h.Id == hotelId);

            if (hotel is null)
            {
                throw new NotFoundException("Hotel not found");
            }

            var roomDtos = _mapper.Map<List<RoomDto>>(hotel.Rooms);

            return roomDtos;
        }

        public RoomDto GetById(int hotelId, int roomId)
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

            var roomDto = _mapper.Map<RoomDto>(room);

            return roomDto;
        }

        public void Update(int hotelId, int roomId, UpdateRoomDto dto)
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

            room.Number = dto.Number;
            room.GuestsNumber = dto.GuestsNumber;
            room.PriceForNight = dto.PriceForNight;

            _context.SaveChanges();
        }

        public void DeleteById(int hotelId, int roomId)
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

            _context.Rooms.Remove(room);
            _context.SaveChanges();
        }

        public void DeleteAll(int hotelId)
        {
            var hotel = _context.Hotels.Include(h => h.Rooms).FirstOrDefault(h => h.Id == hotelId);

            if (hotel is null)
            {
                throw new NotFoundException("Hotel not found");
            }

            _context.RemoveRange(hotel.Rooms);
            _context.SaveChanges(); 
        }
    }
}
