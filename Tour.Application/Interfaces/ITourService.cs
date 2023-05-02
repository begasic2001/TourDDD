using Tour.Application.Dto;
using Tour.Domain.Entities;
namespace Tour.Application.Interfaces
{
    public interface ITourService : IService<Tours, TourDto>
    {
        SearchVM Search(string? search, double? from, double? to, string? sortBy, int page = 1);

    }
}
