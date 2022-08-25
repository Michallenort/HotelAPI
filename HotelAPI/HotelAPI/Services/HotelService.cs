using AutoMapper;
using HotelAPI.Entities;
using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI.Services
{
    public interface IHotelService
    {
        public IEnumerable<HotelDto> GetAll();
    }

    public class HotelService : IHotelService
    {
        private readonly HotelDbContext _dbContext;
        private readonly IMapper _mapper;
        public HotelService(HotelDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<HotelDto> GetAll()
        {
            var hotels = _dbContext.Hotels.Include(r => r.Address).Include(r => r.Rooms).ToList();

            var hotelsDtos = _mapper.Map<List<HotelDto>>(hotels);

            return hotelsDtos;
        }
    }
}
