using Microsoft.AspNetCore.Identity;
using Tour.Application.Dto;
using Tour.Domain.Entities;
namespace Tour.Application.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> SignUpAsync(SignUpDto model);
        Task<string> SignInAsync(SignInDto model);
    }
}
