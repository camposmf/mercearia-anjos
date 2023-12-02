using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.Database;
using web_api.Models;

namespace web_api.Controllers
{
    [ApiController]
    [Route("clients")]
    public class ClientsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ClientsController(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientModel>>> GetClient()
        {
            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<ClientModel>>> CreateClient(ClientModel client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<ClientModel>>> UpdateClient(ClientModel client)
        {
            var clientEntity = await _context.Clients.FindAsync(client.Id);

            if (clientEntity == null)
                return NotFound("Client not found");

            clientEntity.Name = client.Name;
            clientEntity.Email = client.Email;
            clientEntity.Address = client.Address;
            clientEntity.PhoneNumber = client.PhoneNumber;

            await _context.SaveChangesAsync();
            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpDelete("{clientId}")]
        public async Task<ActionResult<List<ClientModel>>> DeleteClient(int clientId)
        {
            var clientEntity = await _context.Clients.FindAsync(clientId);

            if (clientEntity == null)
                return NotFound("Client not found");

            _context.Clients.Remove(clientEntity);
            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ToListAsync());
        }
    }
}
