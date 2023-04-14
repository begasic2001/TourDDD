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
        public async Task<List<UserModel>> GetAllUser()
        {
            using (var client = new HttpClient())
            {
                List<UserModel> UserList = new List<UserModel>();
                var url = "https://localhost:9001/api/User/GetAllUsers";
                client.BaseAddress = new Uri(url);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var strResult = response.Content.ReadAsStringAsync().Result;
                    UserList = JsonConvert.DeserializeObject<List<UserModel>>(strResult);
                    return UserList;
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<string> PostUri(Uri u, HttpContent c)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                //var newPostJson = JsonConvert.SerializeObject(c);
                HttpResponseMessage result = await client.PostAsync(u, c);
                if(result.IsSuccessStatusCode)
                {
                    //StatusCode.ToString();
                    response = result.Content.ReadAsStringAsync().Result;
                }
            }
            return response;
        }
        //public async Task<List<AuthResult>> SignInAsync(SignInDto model)
        //{
        //    List<SignInDto> dataPost = new List<SignInDto>();
        //    var json = JsonConvert.SerializeObject(dataPost);
        //    var data = new StringContent(json, Encoding.UTF8, "application/json");
        //    var url = "https://localhost:9001/api/User/SignIn";
        //    using (var client = new HttpClient())
        //    {
                
        //        List<AuthResult> authResults = new List<AuthResult>();
             
        //        client.BaseAddress = new Uri(url);
        //        HttpResponseMessage response = await client.PostAsync(url, data);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var strResult = response.Content.ReadAsStringAsync().Result;
        //            authResults = JsonConvert.DeserializeObject<List<AuthResult>>(strResult);
        //            Console.WriteLine(strResult);
        //            Console.WriteLine(authResults);
        //            return authResults;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

        //public async Task<string> SignInAsync(SignInDto model)
        //{
        //    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
        //    if (!result.Succeeded)
        //    {
        //        return string.Empty;
        //    }

        //    var authClaims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Email, model.Email),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    };

        //    var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

        //    var token = new JwtSecurityToken(
        //        issuer: configuration["JWT:ValidIssuer"],
        //        audience: configuration["JWT:ValidAudience"],
        //        expires: DateTime.Now.AddDays(1),
        //        claims: authClaims,
        //        signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //public async Task<IdentityResult> SignUpAsync(SignUpDto model)
        //{
        //    var user = new User
        //    {
        //        Email = model.Email,
        //        UserName = model.Email
        //    };

        //    return await userManager.CreateAsync(user, model.Password);
        //}
    }
}
