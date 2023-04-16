using Microsoft.AspNetCore.Identity;
using Tour.Application.Dto;
using Tour.Domain.Entities;
namespace Tour.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> GetUri(Uri u);
        Task<string> PostUri(Uri u, HttpContent c);
    }
}
