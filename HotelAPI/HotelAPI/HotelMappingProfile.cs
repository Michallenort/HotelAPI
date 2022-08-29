using AutoMapper;
using HotelAPI.Entities;
using HotelAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAPI
{
    public class HotelMappingProfile : Profile
    {
        public HotelMappingProfile()
        {
            CreateMap<Hotel, HotelDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street));

            CreateMap<Room, RoomDto>();

            CreateMap<CreateHotelDto, Hotel>()
                .ForMember(h => h.Address,
                c => c.MapFrom(dto => new Address()
                {
                    City = dto.City,
                    Street = dto.Street,
                    PostalCode = dto.PostalCode
                }));

            CreateMap<CreateRoomDto, Room>();

            CreateMap<CreateReservationDto, Reservation>();
        }
    }
}
