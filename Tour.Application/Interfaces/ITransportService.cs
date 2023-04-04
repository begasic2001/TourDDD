using Tour.Application.Dto;
using Tour.Domain.Entities;
namespace Tour.Application.Interfaces
{
    public interface ITransportService : IService<Transport, TransportDto>
    {
    }
}
