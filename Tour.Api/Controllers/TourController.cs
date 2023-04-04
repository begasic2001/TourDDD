using Microsoft.AspNetCore.Mvc;
using Tour.Application.Dto;
using Tour.Application.Interfaces;
using Tour.Infrastructure.Common;
using Tour.Infrastructure.Data;

namespace Tour.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly ITourService _tourService;
        
        private readonly TourDatabaseContext _context;
        //private readonly ILoggerService _logger;

        public TourController(ITourService tourService, TourDatabaseContext context )
        {
            _tourService = tourService;
            _context = context;
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //_logger.LogInfo("Get All Tour !!!!!!!");
                var a = await _tourService.GetAll();

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
                //_logger.LogInfo($"Get tour by id {id}");
                var a = await _tourService.GetByIdAsync(id);
                return Ok(a);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(TourDto tour)
        {
            try
            {
                //_logger.LogInfo("Create new tour !!!!");
                var hasTour = _context.Tour.Where(c => c.Name.Trim().ToLower() == tour.Name.Trim().ToLower()).FirstOrDefault();
                if (hasTour != null)
                {
                    return Conflict($"{tour.Name} already exists!");
                }
                tour.Name = tour.Name.Trim().ToLower();
                await _tourService.AddAsync(tour);
                
                //_logger.LogInfo("Create successfull !!!");
                return Ok("Created !!!!");
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, TourDto tour)
        {
            try
            {
                //_logger.LogInfo($"Edit tour by id {id}");
                if (tour.Id != id)
                {
                    return NotFound(id);
                }
               

                tour.Name = tour.Name.Trim().ToLower();
                await _tourService.UpdateAsync(id, tour);
                
                //_logger.LogInfo($"Update succesfull {id}");
                return Ok("Updated !!!!");
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
                //_logger.LogInfo($"Delete tour by id {id}");

                await _tourService.DeleteAsync(id);
                
                //_logger.LogInfo($"Delete Successful id {id}");
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
