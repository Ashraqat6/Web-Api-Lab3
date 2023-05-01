using lab_2.DAL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace lab_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly UserManager<Employee> _userManager;

        public EmployeeController(UserManager<Employee> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetUserInfo()
        {
            var user = await _userManager.GetUserAsync(User);

            return Ok(new string[] { 
            user!.UserName!,
            user!.Email!,
            user.Hobby
        });
        }

        [HttpGet]
        [Authorize(Policy = "Admins")]
        [Route("ForAdmins")]
        public ActionResult GetInfoForAdmins()
        {
            return Ok("This Data For Admins & Employees Only");
        }

        [HttpGet]
        [Authorize(Policy = "Employees")]
        [Route("ForEmployees")]
        public ActionResult GetInfoForEmployees()
        {
            return Ok("This Data For Employees");
        }
    }
}
