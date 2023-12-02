using web_api.Models;
using web_api.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace web_api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UsersController(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<UserModel>>> CreateUser(UserModel user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<UserModel>>> UpdateUser(UserModel user)
        {
            var userEntity = await _context.Users.FindAsync(user.Id);

            if (userEntity == null)
                return NotFound("User not found.");

            userEntity.Age = user.Age;
            userEntity.Name = user.Name;
            userEntity.City = user.City;

            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<List<UserModel>>> DeleteUser(int userId)
        {
            var userEntity = await _context.Users.FindAsync(userId);

            if (userEntity == null)
                return NotFound("User not found");

            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

    }
}
