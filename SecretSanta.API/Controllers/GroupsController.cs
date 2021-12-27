using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.API.Domain.Interfaces;
using SecretSanta.API.Domain.Models;

namespace SecretSanta.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;

        public GroupsController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            try
            {
                var result = await _groupRepository.GetAll();
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup(string id)
        {
            try
            {
                var group = new Group { Id = id };
                var result = await _groupRepository.Get(group);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] Group group)
        {
            try
            {
                var result = await _groupRepository.Add(group);
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