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

        [HttpGet]
        [SwaggerOperation(Tags = new[] { "user" })]
        public async Task<IActionResult> GetUsers([FromServices] IUserService userService)
        {
            // validation

            try
            {
                var users = await userService.GetUsers();
                return Ok(users);
            } catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("regex")]
        [SwaggerOperation(Tags = new[] { "configuration" })]
        public async Task<IActionResult> GetRegex([FromServices] IUserService userService)
        {
            // validation

            try
            {
                var regex = userService.GetRegex();
                return Ok(regex);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("update")]
        [SwaggerOperation(Tags = new[] { "user" })]
        public async Task<IActionResult> UpdateUser([FromServices] IUserService userService, [FromBody] RequestUserModel userModel)
        {
            // validation

            try
            {
                var users = await userService.UpdateUser(userModel);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
