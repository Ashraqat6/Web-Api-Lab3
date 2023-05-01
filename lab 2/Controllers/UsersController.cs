using lab_2.BL.DTOs.Employees;
using lab_2.DAL.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace lab_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Employee> _userManager;

        public UsersController(IConfiguration configuration,
            UserManager<Employee> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        #region Admin Register

        [HttpPost]
        [Route("AdminRegister")]
        public async Task<ActionResult> AdminRegister(RegisterDto registerDto)
        {
            var newEmployee = new Employee
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Hobby = registerDto.Hobby,
            };

            var creationResult = await _userManager.CreateAsync(newEmployee,
                registerDto.Password);
            if (!creationResult.Succeeded)
            {
                return BadRequest(creationResult.Errors);
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, newEmployee.Id),
            new Claim(ClaimTypes.Role, "Admin")
        };

            await _userManager.AddClaimsAsync(newEmployee, claims);

            return NoContent();
        }

        #endregion

        #region Employee Register

        [HttpPost]
        [Route("EmployeeRegister")]
        public async Task<ActionResult> EmployeeRegister(RegisterDto registerDto)
        {
            var newEmployee = new Employee
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Hobby = registerDto.Hobby,
            };

            var creationResult = await _userManager.CreateAsync(newEmployee,
                registerDto.Password);
            if (!creationResult.Succeeded)
            {
                return BadRequest(creationResult.Errors);
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, newEmployee.Id),
            new Claim(ClaimTypes.Role, "Employee")
        };

            await _userManager.AddClaimsAsync(newEmployee, claims);

            return NoContent();
        }

        #endregion

        #region Login

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<TokenDto>> Login(LoginDto credentials)
        {
            Employee? user = await _userManager.FindByNameAsync(credentials.UserName);
            if (user == null)
            {               
                return BadRequest();
            }

            bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, credentials.Password);
            if (!isPasswordCorrect)
            {                
                return BadRequest();
            }

            var claimsList = await _userManager.GetClaimsAsync(user);

            return GenerateToken(claimsList);
        }

        #endregion

        private TokenDto GenerateToken(IList<Claim> claimsList)
        {
            string keyString = _configuration.GetValue<string>("SecretKey") ?? string.Empty;
            var keyInBytes = Encoding.ASCII.GetBytes(keyString);
            var key = new SymmetricSecurityKey(keyInBytes);

            //Combination of secret Key and HashingAlgorithm
            var signingCredentials = new SigningCredentials(key,
                SecurityAlgorithms.HmacSha256Signature);

            //Putting All together
            var expiry = DateTime.Now.AddHours(1);

            var jwt = new JwtSecurityToken(
                    expires: expiry,
                    claims: claimsList,
                    signingCredentials: signingCredentials);

            //Getting Token String
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(jwt);

            return new TokenDto
            {
                Token = tokenString,
                Expiry = expiry
            };
        }
    }
}
