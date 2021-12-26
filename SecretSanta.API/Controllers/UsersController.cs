using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.API.Models;
using SecretSanta.API.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;


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

        // [HttpPost("verify")]
        // public async Task<IActionResult> VerifyToken(TokenV request)
        // {
        //     var auth = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;
        //
        //     try
        //     {
        //         var response = await auth.VerifyIdTokenAsync(request.)
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e);
        //         throw;
        //     }
        // }

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
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}