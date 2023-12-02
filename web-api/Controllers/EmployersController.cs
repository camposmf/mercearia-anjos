using web_api.Models;
using web_api.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace web_api.Controllers
{
    [ApiController]
    [Route("employers")]
    public class EmployersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public EmployersController(DatabaseContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployerModel>>> GetEmploeyers()
        {
            return Ok(await _context.Employers.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<EmployerModel>>> CreateEmployer(EmployerModel employer)
        {
            _context.Add(employer);
            await _context.SaveChangesAsync();
            return Ok(await _context.Employers.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<EmployerModel>>> UpdateEmployer(EmployerModel employer)
        {
            var employerEntity = await _context.Employers.FindAsync(employer.Id);

            if (employerEntity == null)
                return NotFound("Employer not found");

            employerEntity.Name = employer.Name;
            employerEntity.Login = employer.Login;
            employerEntity.Password = employer.Password;
            employerEntity.PhoneNumber = employer.PhoneNumber;

            await _context.SaveChangesAsync();
            return Ok(await _context.Employers.ToListAsync());
        }

        [HttpDelete("employerId")]
        public async Task<ActionResult<List<EmployerModel>>> DeleteEmployer(int employerId)
        {
            var employerEntity = await _context.Employers.FindAsync(employerId);

            if (employerEntity == null)
                return NotFound("Employer not found");

            _context.Employers.Remove(employerEntity);
            await _context.SaveChangesAsync();

            return Ok(await _context.Employers.ToListAsync());
        }
    }
}
