using APIWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParticipationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ParticipationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("participation")]
        public async Task<IActionResult> CreateParticipation(Participation participation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Participations.Add(participation);
            await _context.SaveChangesAsync();
            return Ok("Participation created successfully");
        }


        [HttpGet("participation/{adminID}")]
        public async Task<IActionResult> GetParticipationByAdmin(string adminID)
        {
            var participation = await _context.Participations
                .Where(p => p.AdminID == adminID)
                .Select(p => new
                {
                    p.RoomName,
                    p.AdminName,
                    p.Amount,
                    p.Time
                })
                .ToListAsync();

            return Ok(participation);
        }

        [HttpGet("document/{receiverID}")]
        public async Task<IActionResult> GetDocumentsByReceiver(string receiverID)
        {
            var documents = await _context.MyDocuments
                 .Where(d => d.ReceiverID == receiverID)
                .Select(d => new
                {
                    d.Name,
                    d.File,
                    d.Id
                })
                .ToListAsync();

            return Ok(documents);
        }
        [HttpPost("document")]
        public async Task<IActionResult> CreateDocument(MyDocument document)
        {
            _context.MyDocuments.Add(document);
            await _context.SaveChangesAsync();
            return Ok("Document created successfully");
        }
        [HttpPost("service")]
        public async Task<IActionResult> CreateService([FromBody] Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Services.Add(service);

            if (service.entityServices != null)
            {
                foreach (var entityService in service.entityServices)
                {
                    var existingService = await _context.Services.FindAsync(entityService.ServiceId);
                    if (existingService == null)
                    {
                        return BadRequest("Service not found");
                    }
                    entityService.ServiceId = existingService.Id; 
                    _context.EntityServices.Add(entityService);
                }
            }

            await _context.SaveChangesAsync();
            return Ok("Service created successfully");
        }



        [HttpGet("service/{serviceID}")]
        public async Task<IActionResult> GetServiceByID(string serviceID)
        {
            var service = await _context.Services.FindAsync(serviceID);
            if (service == null)
                return NotFound("Service not found");

            var serviceDto = new
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Cost = service.Cost,
                Image = service.Image,
                EntityServices = service.entityServices
            };

            return Ok(serviceDto);
        }
        [HttpPost("entity-service")]
        public async Task<IActionResult> CreateEntityService(EntityService entityService)
        {
            _context.EntityServices.Add(entityService);
            await _context.SaveChangesAsync();
            return Ok("Entity service created successfully");
        }
    }
}