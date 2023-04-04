using Microsoft.AspNetCore.Mvc;
using Tour.Application.Dto;
using Tour.Application.Interfaces;
using Tour.Infrastructure.Common;
using Tour.Infrastructure.Data;

namespace Tour.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SightController : ControllerBase
    {
        private readonly ISightService _sightService;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly ILoggerService _logger;
        private readonly TourDatabaseContext _context;

        public SightController(ISightService sightService, TourDatabaseContext context)
        {
            _sightService = sightService;
            
            //_logger = logger;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //_logger.LogInfo("Get all Sight !!!!");
                var res = _sightService.GetAllJoin(new string[] { "City" }).Select(x =>
                   new
                   {
                       Id = x.Id,
                       SightName = x.SightName,
                       SightForMoney = x.SightForMoney,
                       Picture = x.Picture,
                       CityName = x.City.CityName
                   }
               );
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
                //_logger.LogInfo($"Get sight by id {id} !!!!");
                var res = _sightService.GetMultiJoin(c => c.Id == id, new string[] { "City" }).Select(x =>
                   new
                   {
                       Id = x.Id,
                       SightName = x.SightName,
                       SightForMoney = x.SightForMoney,
                       Picture = x.Picture,
                       CityName = x.City.CityName
                   }
               );
                return Ok(res);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(SightDto sight)
        {
            try
            {
                //_logger.LogInfo("Create new sight !!!!!");
                var hasSight = _context.Sight.Where(c => c.SightName.Trim().ToLower() == sight.SightName.Trim().ToLower()).FirstOrDefault();
                if (hasSight != null)
                {
                    return Conflict($"{sight.SightName} already exists!");
                }
                sight.SightName = sight.SightName.Trim().ToLower();
                await _sightService.AddAsync(sight);
                
                //_logger.LogInfo($"Create successfull");
                var newSight = await _sightService.GetByIdAsync(sight.Id);
                return newSight == null ? NotFound() : Ok(newSight);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, SightDto sight)
        {
            try
            {
                //_logger.LogInfo($"Edit sight by id {id}");
                if (sight.Id != id)
                {
                    return NotFound(id);
                }
               
                sight.SightName = sight.SightName.Trim().ToLower();
                await _sightService.UpdateAsync(id, sight);
                
                //_logger.LogInfo($"Update successful {id}");
                var editSight = await _sightService.GetByIdAsync(sight.Id);
                return editSight == null ? NotFound() : Ok(editSight);
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
                //_logger.LogInfo($"Delete sight by id {id}");

                await _sightService.DeleteAsync(id);
                await _unitOfWork.SaveChangesAsync();
                //_logger.LogInfo($"Delete successful {id}");
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
