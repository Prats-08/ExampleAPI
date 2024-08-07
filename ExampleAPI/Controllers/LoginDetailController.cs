using ExampleAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExampleAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginDetailController : ControllerBase
    {
        private readonly DetailDbContext _dbContext;

        public LoginDetailController(DetailDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        [HttpGet("{loginName}")]
        public  ActionResult<LoginDetail> GetPasswordById(string loginName)
        {
            if (_dbContext.LoginDetails == null)
            {
                return NotFound();
            }
            var password =  _dbContext.LoginDetails.Where(x => x.loginName == loginName).FirstOrDefault();
            if (password == null)
            {
                return NotFound();
            }
            return password;
        }
        [HttpPost]
        public async Task<ActionResult<LoginDetail>> PostLoginDetail(LoginDetail login)
        {
            _dbContext.LoginDetails.Add(login);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(login), new { employeeId = login.employeeId }, login);
        }
    }
}
