using ExampleAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace ExampleAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmplyeeDetailController : ControllerBase
    {
        private readonly DetailDbContext _dbContext;

        public EmplyeeDetailController(DetailDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDetail>>> GetEmployeeDetails()
        { 
            if (_dbContext.Employees == null)
            {
                return NotFound();
            }
            return await _dbContext.Employees.ToListAsync();
        }
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeDetail>> GetEmployeeDetailById(int employeeId)
        {
            if (_dbContext.Employees == null)
            {
                return NotFound();
            }
            var employee = await _dbContext.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }
        [HttpPost]
        public async Task<ActionResult<EmployeeDetail>> PostEmployeeDetail(EmployeeDetail employee)
        {
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(employee), new { employeeId = employee.employeeId }, employee);
        }
        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(int employeeId, EmployeeDetail employee)
        {
            if (employeeId != employee.employeeId)
            {
                return BadRequest();
            }
            _dbContext.Entry(employee).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeAvailable(employeeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }
        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            if (_dbContext.Employees == null)
            {
                return NotFound();
            }
            var food = await _dbContext.Employees.FindAsync(employeeId);
            if (food == null)
            {
                return NotFound();
            }
            _dbContext.Employees.Remove(food);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        private bool EmployeeAvailable(int employeeId)
        {
            return (_dbContext.Employees?.Any(x => x.employeeId == employeeId)).GetValueOrDefault();
        }
    }
}
