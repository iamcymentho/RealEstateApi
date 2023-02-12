using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealEstateApi.Data;
using RealEstateApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        ApiDbContext _dbContext = new ApiDbContext(); // instantiating database connector  (dbconn)

        private IConfiguration _config; // Helps get access to all configurations in the AppSettings.Json

        public UsersController(IConfiguration config) // dependency injection
        {
            _config= config;
        }


        [HttpPost("[action]")]
        public IActionResult Register([FromBody] User user)
        {
           var UserEmailExists = _dbContext.Users.FirstOrDefault(u=>u.EmailAddress == user.EmailAddress); // Checking if user email exists in the users table database

            if (UserEmailExists != null)
            {
                return BadRequest("User with email address already exists. Please try again with a new email");
            }
            else
            {
                _dbContext.Users.Add(user);  // if user doesnt exists , add use to database 
                _dbContext.SaveChanges();

                //return Ok("User successfully registered");

                return StatusCode(StatusCodes.Status201Created);
            }
        }

        [HttpPost("[action]")]
        public IActionResult Login([FromBody] User user)
        {
            var userDetails = _dbContext.Users.FirstOrDefault(u => u.EmailAddress == user.EmailAddress && u.Password == user.Password); // checking for user email and password against the Database

            if (userDetails == null) 
            { 
                return NotFound("Invalid login details");
            }
            // else
            //{

            //}

            // generating JWT token referencing the KEY registered in the AppSettings !

             var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));

            // harshing the security key 

            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            // payload that will be sent to the JWT will be registered in CLAIM 

            var claims = new[]
            {
                new  Claim(ClaimTypes.Email, user.EmailAddress)
            };

            var token = new JwtSecurityToken (
                
                issuer : _config["JWT:Issuer"],
                audience : _config["JWT:audience"],
                claims : claims,
                expires :DateTime.Now.AddMinutes(5),
                signingCredentials : credentials

                );

           var jwtToken =  new JwtSecurityTokenHandler().WriteToken(token); // this line generates token in a string form 

            return Ok(jwtToken);

        }


    }
}
