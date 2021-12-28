using System;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.API.Domain.Interfaces;
using SecretSanta.API.Domain.Models;

namespace SecretSanta.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserWorkflow _userWorkflow;

        public UsersController(IUserRepository userRepository, IUserWorkflow userWorkflow)
        {
            _userRepository = userRepository;
            _userWorkflow = userWorkflow;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
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
        public async Task<IActionResult> Get(string id)
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
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            try
            {
                var result = await _userRepository.SignUp(user);
                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception("Failure to CreateUserAsync: " + e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] User user)
        {
            try
            {
                var result = await _userWorkflow.HandleSignIn(user);
                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception("Failure to SignIn user: " + e);
            }
        }
    }
}