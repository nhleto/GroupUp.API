using System;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.API.Models;
using SecretSanta.API.Models.Interfaces;

namespace SecretSanta.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var result = await _userRepository.GetAll();
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            try
            {
                var user = new User { Id = id };
                var result = await _userRepository.Get(user);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            try
            {
                var result = await _userRepository.Add(user);
                var uid = result.Id;
                var customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(uid);
                
                return Ok(customToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}