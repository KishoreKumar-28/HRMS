using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HRMS.Models;
using HRMS.Provider;
using HRMS.Data;
using Microsoft.AspNetCore.Authorization;
using HRMS.Repository;
using System.Security.Cryptography;
using System.Text;
//using HRMS.Services;

namespace HRMS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //private ILog _logger;
        private readonly IEmployeeProvider _provider;
        private readonly HrmsdbContext _context;
        private readonly IEmployeeRepository _repo;
        public LoginController(HrmsdbContext context, IEmployeeProvider provider,IEmployeeRepository repository)
        {
            _repo= repository;
            //_logger= logger;
            _context = context;
            _provider = provider;
        }
        [HttpPost]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequest request)
        {
            var user = _context.Employees.FirstOrDefault(x => x.EmailId == request.Email && x.IsActive == true);
            // Checking the email and password is matching with the DB.
            if(user==null)
            {
                return NotFound("User not found");
            }
            var decryptedPassword = _repo.VerifyPasswordhash(request.password,user.PasswordHash,user.PasswordSalt);
            if (!decryptedPassword)
            //if (user is null || user.IsActive == false)
            {
                return BadRequest("Invalid credentials.");
            }
            // Creating claims to check the Id and roles in authorization.
            var claims = new List<Claim>
            {
                 new Claim(type: ClaimTypes.Email, value: request.Email),
                 new Claim(type: ClaimTypes.Name,value: user.FirstName),
                 new Claim(type: ClaimTypes.NameIdentifier,value: user.EmployeeId.ToString()),
                 new Claim(type: ClaimTypes.Role,value: user.Roles),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity),
            new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            });
            //_logger.Information($"{user.FirstName} signed in");
            return Ok("Signed in successfully");
        }
        // Employee Login.
        //[HttpPost]
        //public async Task<IActionResult> SignInAsync([FromBody] SignInRequest request)
        //{
        //    var user = _context.Employees.FirstOrDefault(x => x.EmailId == request.Email && x.IsActive == true);
        //    // Checking the email and password is matching with the DB.
        //    var decryptedPassword = _provider.Decrypt(user.Password);
        //    if(!(decryptedPassword == request.password))
        //    //if (user is null || user.IsActive == false)
        //    {
        //        return BadRequest("Invalid credentials.");
        //    }
        //    // Creating claims to check the Id and roles in authorization.
        //    var claims = new List<Claim>
        //    {
        //         new Claim(type: ClaimTypes.Email, value: request.Email),
        //         new Claim(type: ClaimTypes.Name,value: user.FirstName),
        //         new Claim(type: ClaimTypes.NameIdentifier,value: user.EmployeeId.ToString()),
        //         new Claim(type: ClaimTypes.Role,value: user.Roles),
        //    };
        //    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        //    await HttpContext.SignInAsync(
        //    CookieAuthenticationDefaults.AuthenticationScheme,
        //    new ClaimsPrincipal(identity),
        //    new AuthenticationProperties
        //    {
        //        IsPersistent = true,
        //        AllowRefresh = true,
        //        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
        //    });
        //    //_logger.Information($"{user.FirstName} signed in");
        //    return Ok("Signed in successfully");
        //}
        // By using the claim details it will get the employee by Id.
        [HttpGet]
        public ActionResult<Employee> GetMyDetails()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                //_logger.Warning("Trying to view details without login succesfully");
                return BadRequest("Login to view your details");
            }
            var employee = _provider.GetEmployee(userId);
            //_logger.Information($"{User.FindFirstValue(ClaimTypes.Name)} viewed their details");
            return Ok(employee);
        }
        // It will clear the claim details when clicking signout.
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
           // _logger.Information($"{User.FindFirstValue(ClaimTypes.Name)} signed out succesfully");
            return Ok("Signed out successfully");
        }
 
    }
}
