using Microsoft.AspNetCore.Mvc;
using Tour.Application.Dto;
using Tour.Application.Interfaces;
using Tour.Infrastructure.Common;
using Tour.Infrastructure.Data;

namespace Tour.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        
        //private readonly ILoggerService _logger;
        private readonly TourDatabaseContext _context;

        public CityController(ICityService cityService,   TourDatabaseContext context)
        {
            _cityService = cityService;
            
            //_logger = logger;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //_logger.LogInfo("Get All City !!!!");
                var res = _cityService.GetAllJoin(new string[] { "Country" }).Select(x =>
                    new
                    {
                        Id = x.Id,
                        CityName = x.CityName,
                        CountryName = x.Country.CountryName
                    }
                );
                //_logger.LogInfo($"Get List ${res}");
                return Ok(res);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                //_logger.LogInfo($"Get City By Id  ${id}");
                var res = _cityService.GetMultiJoin(c => c.Id == id, new string[] { "Country" }).Select(x =>
                    new
                    {
                        Id = x.Id,
                        CityName = x.CityName,
                        CountryName = x.Country.CountryName
                    }
                );
                //_logger.LogInfo($"Get List ${res}");
                return Ok(res);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(CityDto city)
        {
            try
            {
                //_logger.LogInfo("Create New City");
                var hasCity = _context.City.Where(c => c.CityName.Trim().ToLower() == city.CityName.Trim().ToLower()).FirstOrDefault();
                if (hasCity != null)
                {
                    return Conflict($"{city.CityName} already exists!");
                }
                city.CityName = city.CityName.Trim().ToLower();
                await _cityService.AddAsync(city);
                
                //_logger.LogInfo("City Save Successfully!!");
                var newCity = await _cityService.GetByIdAsync(city.Id);
                return newCity == null ? NotFound() : Ok(newCity);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, CityDto city)
        {
            try
            {
                //_logger.LogInfo($"Update City By Id {id}");
                if (city.Id != id)
                {
                    return NotFound(id);
                }
                //var hasCityByid = _context.City.Where(c => c.CityName.Trim().ToLower() == city.CityName.Trim().ToLower() && c.Id == id).FirstOrDefault();
                //if (hasCityByid != null)
                //{
                //    return Conflict($"{city.CityName} already exists!");
                //}
                city.CityName = city.CityName.Trim().ToLower();
                await _cityService.UpdateAsync(id, city);
                
                //_logger.LogInfo($"Edit Successfully City By Id {id}");
                var editCity = await _cityService.GetByIdAsync(city.Id);
                return editCity == null ? NotFound() : Ok(editCity);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {

                //_logger.LogInfo($"Delete City By Id {id}");

                await _cityService.DeleteAsync(id);
                
                //_logger.LogInfo($"Delete Successful {id}");
                return Ok($"Delete Successful {id}");


            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }
    }
}
