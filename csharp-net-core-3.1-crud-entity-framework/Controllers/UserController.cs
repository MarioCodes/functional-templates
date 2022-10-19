using crud.Models;
using crud.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace crud.Controllers
{
    // add api versioning
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [SwaggerOperation(Tags = new[] { "user" })]
        public async Task<IActionResult> GetUsers()
        {
            // validation

            try
            {
                var users = await _userService.GetUsers();
                return Ok(users);
            } catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("regex")]
        [SwaggerOperation(Tags = new[] { "configuration" })]
        public async Task<IActionResult> GetRegex()
        {
            // validation

            try
            {
                var regex = await _userService.GetRegex();
                return Ok(regex);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("update")]
        [SwaggerOperation(Tags = new[] { "user" })]
        public async Task<IActionResult> UpdateUser([FromBody] RequestUserModel userModel)
        {
            // validation

            try
            {
                var users = await _userService.UpdateUser(userModel);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
