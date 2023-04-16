using AutoMapper;
using Tour.Infrastructure.Data;
using Tour.Infrastructure.Repositories;

using Tour.Application.Interfaces;
using Tour.Domain.Entities;
using Tour.Application.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Tour.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {
           
        }
        public async Task<string> GetUri(Uri u)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
              
                HttpResponseMessage result = await client.GetAsync(u);
                if (result.IsSuccessStatusCode)
                {
                    
                    response = result.Content.ReadAsStringAsync().Result;
                }
                return response;
            }
        }
        public async Task<string> PostUri(Uri u, HttpContent c)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = await client.PostAsync(u, c);
                if(result.IsSuccessStatusCode)
                {
                    response = result.Content.ReadAsStringAsync().Result;
                }
            }
            return response;
        }

    }
}
