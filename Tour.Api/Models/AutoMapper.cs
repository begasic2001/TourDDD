using AutoMapper;
using Tour.Application.Dto;
using Tour.Domain.Entities;

namespace Tour.Infrastructure.Common
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<Sight, SightDto>().ReverseMap();
            CreateMap<Transport, TransportDto>().ReverseMap();
            CreateMap<Tours, TourDto>().ReverseMap();
        }
    }
}
