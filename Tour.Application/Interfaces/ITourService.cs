using Tour.Application.Dto;
using Tour.Domain.Entities;
namespace Tour.Application.Interfaces
{
    public interface ITourService : IService<Tours, TourDto>
    {
        //Task<IEnumerable<object>> GetJoin();
        //Task<Tours> GetJoinById(string id);
        //Task AddAsyncJoin(TourDto tour);
        //Task UpdateAsyncJoin(string id, TourDto tour);

    }
}
