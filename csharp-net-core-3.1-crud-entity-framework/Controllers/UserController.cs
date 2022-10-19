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
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
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

        [HttpGet("user/{userId}")]
        [SwaggerOperation(Tags = new[] { "user" })]
        public async Task<IActionResult> GetUser(string userId)
        {
            // TODO: Add logging.

            if(!(await _userService.ValidateUserId(userId)))
            {
                return StatusCode(500, "userID must be a positive number");
            }

            try
            {
                var user = await _userService.GetSpecificUser(int.Parse(userId));
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("user/regex")]
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

        [HttpPost("user/update")]
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
