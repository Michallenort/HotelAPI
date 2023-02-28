using AutoMapper;
using HotelAPI.Authorization;
using HotelAPI.Entities;
using HotelAPI.Exceptions;
using HotelAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace HotelAPI.Services
{
    public interface IHotelService
    {
        IEnumerable<HotelDto> GetAll();
        HotelDto GetById(int id);
        int Create(CreateHotelDto dto);
        void Update(int id, UpdateHotelDto dto);
        void Delete(int id);
    }

    public class HotelService : IHotelService
    {
        private readonly HotelDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public HotelService(HotelDbContext dbContext, IMapper mapper, ILogger<HotelService> logger, IAuthorizationService authorizationService,
            IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public IEnumerable<HotelDto> GetAll()
        {
            var hotels = _dbContext.Hotels.Include(r => r.Address).Include(r => r.Rooms).ToList();

            var hotelsDtos = _mapper.Map<List<HotelDto>>(hotels);

            return hotelsDtos;
        }

        public HotelDto GetById(int id)
        {
            var hotel = _dbContext.Hotels.Include(r => r.Address).Include(r => r.Rooms).FirstOrDefault(h => h.Id == id);

            if (hotel is null)
                throw new NotFoundException("Hotel not found");

            var hotelDto = _mapper.Map<HotelDto>(hotel);

            return hotelDto;
        }

        public int Create(CreateHotelDto dto)
        {
            var hotel = _mapper.Map<Hotel>(dto);
            hotel.CreatedById = _userContextService.GetUserId;
            _dbContext.Hotels.Add(hotel);
            _dbContext.SaveChanges();

            return hotel.Id;

        }

        public void Update(int id, UpdateHotelDto dto)
        {
            var hotel = _dbContext.Hotels.FirstOrDefault(h => h.Id == id);

            if (hotel is null)
                throw new NotFoundException("Hotel not found");

            var authorizationResult = 
                _authorizationService.AuthorizeAsync(_userContextService.User, 
                    hotel, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            hotel.Name = dto.Name;
            hotel.Description = dto.Description;
            hotel.ContactEmail = dto.ContactEmail;
            hotel.ContactNumber = dto.ContactNumber;

            _dbContext.SaveChanges();

        }

        public void Delete(int id)
        {
            var hotel = _dbContext.Hotels.FirstOrDefault(h => h.Id == id);

            if (hotel is null)
                throw new NotFoundException("Hotel not found");

            var authorizationResult =
                _authorizationService.AuthorizeAsync(_userContextService.User,
                    hotel, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _dbContext.Hotels.Remove(hotel);
            _dbContext.SaveChanges();


        }
    }
}
