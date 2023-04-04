using Tour.Application.Dto;
using Tour.Domain.Entities;

namespace Tour.Application.Interfaces
{
    public interface ICityService : IService<City, CityDto>
    {
    }
}
