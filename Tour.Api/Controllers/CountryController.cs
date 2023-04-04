using Microsoft.AspNetCore.Mvc;
using Tour.Application.Dto;
using Tour.Application.Interfaces;
using Tour.Infrastructure.Data;

namespace Tour.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        //private readonly ILoggerService _logger;
        private readonly TourDatabaseContext _context;

        public CountryController(ICountryService countryService, TourDatabaseContext context)
        {
            _countryService = countryService;

            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //_logger.LogInfo("Get All Country !!");
                var a = await _countryService.GetAll();
                return Ok(a);
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
                //_logger.LogInfo($"Get Country By Id={id}");
                var countryById = await _countryService.GetByIdAsync(id);
                //_logger.LogInfo($"Id: {id}");
                return countryById == null ? NotFound(id) : Ok(countryById);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(CountryDto country)
        {
            try
            {
                //_logger.LogInfo("Add New Country From Request");
                var hasCountry = _context.Country.Where(c => c.CountryName.Trim().ToLower() == country.CountryName.Trim().ToLower()).FirstOrDefault();
                if (hasCountry != null)
                {
                    return Conflict($"{country.CountryName} already exists!");
                }
                country.CountryName = country.CountryName.Trim().ToLower();
                await _countryService.AddAsync(country);

                //_logger.LogInfo($"Add Successfully ${country}");
                var newCountry = await _countryService.GetByIdAsync(country.Id);
                return newCountry == null ? NotFound() : Ok(newCountry);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, CountryDto country)
        {
            try
            {
                //_logger.LogInfo($"Edit country by id {id}");
                if (country.Id != id)
                {
                    return NotFound(id);
                }


                country.CountryName = country.CountryName.Trim().ToLower();
                await _countryService.UpdateAsync(id, country);
                //_logger.LogInfo($"Update Successfully {id}");
                var editCountry = await _countryService.GetByIdAsync(country.Id);
                return editCountry == null ? NotFound() : Ok(editCountry);
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
                //_logger.LogInfo($"Delete Country ${id}");


                await _countryService.DeleteAsync(id);

                //_logger.LogInfo($"Delete Successfully {id}");
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
