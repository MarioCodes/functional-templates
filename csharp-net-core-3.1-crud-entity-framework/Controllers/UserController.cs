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
            // TODO: Add logging.

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
        public async Task<IActionResult> GetSpecificUser(string userId)
        {
            // TODO: Add logging.

            // has flaws. Is just an example.
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

        [HttpPost("user/insert")]
        [SwaggerOperation(Tags = new[] { "user" })]
        public async Task<IActionResult> InsertUser([FromBody] RequestUserModel userModel)
        {
            // logging

            try
            {
                if((await _userService.UserExists(userModel)))
                {
                    return StatusCode(500, "there's already an user with that email");
                }

                var users = await _userService.InsertUser(userModel);
                return Ok(users);
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
            // logging

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
