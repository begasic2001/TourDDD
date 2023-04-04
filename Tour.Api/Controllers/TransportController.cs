using Microsoft.AspNetCore.Mvc;
using Tour.Application.Dto;
using Tour.Application.Interfaces;
using Tour.Infrastructure.Common;
using Tour.Infrastructure.Data;

namespace Tour.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly ITransportService _transportService;
       
        private readonly TourDatabaseContext _context;

        public TransportController(ITransportService transportService,  TourDatabaseContext context)
        {
            _transportService = transportService;
            
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //_logger.LogInfo("Get all transport!!!!");
                var a = await _transportService.GetAll();
                return Ok(a);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                //_logger.LogInfo($"Get transport by id {id}");
                var a = await _transportService.GetByIdAsync(id);
                return Ok(a);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(TransportDto transport)
        {
            try
            {
                //_logger.LogInfo("Create new transport!!!!");
                var hasTransport = _context.Transport.Where(c => c.TransportName.Trim().ToLower() == transport.TransportName.Trim().ToLower()).FirstOrDefault();
                if (hasTransport != null)
                {
                    return Conflict($"{transport.TransportName} already exists!");
                }
                transport.TransportName = transport.TransportName.Trim().ToLower();
                await _transportService.AddAsync(transport);
                
                //_logger.LogInfo("Create Successfully transport!!!!");
                var newTransport = await _transportService.GetByIdAsync(transport.Id);
                return newTransport == null ? NotFound() : Ok(newTransport);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, TransportDto transport)
        {
            try
            {
                //_logger.LogInfo($"Edit transport by id {id}");
                if (transport.Id != id)
                {
                    return NotFound(id);
                }
              
                transport.TransportName = transport.TransportName.Trim().ToLower();
                await _transportService.UpdateAsync(id, transport);
                
                var editTransport = await _transportService.GetByIdAsync(transport.Id);
                return editTransport == null ? NotFound() : Ok(editTransport);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                //_logger.LogInfo($"Delete transport by id {id}");

                await _transportService.DeleteAsync(id);
                
                //_logger.LogInfo($"Delete successfully {id}");
                return Ok($"Delete Successful {id}");


            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                return BadRequest();
            }

        }
    }
}
