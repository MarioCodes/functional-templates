using testFeatures.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using Services.interfaces;
using System.Collections.Generic;

namespace testFeatures.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {

        [HttpGet]
        [SwaggerOperation(Tags = new[] { "user" })]
        public async Task<IActionResult> GetUsers([FromServices] IUserService userService)
        {
            try
            {
                var users = await userService.GetUsers();
                return Ok(users);
            } catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getSoftDeletedUsers")]
        [SwaggerOperation(Tags = new[] { "user" })]
        public async Task<IActionResult> GetDeletedUsers([FromServices] IUserService userService)
        {
            try
            {
                var users = await userService.GetSoftDeletedUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("regex")]
        [SwaggerOperation(Tags = new[] { "configuration" })]
        public async Task<IActionResult> GetRegex([FromServices] IUserService userService)
        {
            try
            {
                var regex = await userService.GetRegex();
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

        [HttpPost("delete")]
        [SwaggerOperation(Tags = new[] { "user" })]
        public async Task<IActionResult> DeleteUser([FromServices] IUserService userService, [FromBody] List<RequestUserModel> userModel)
        {
            try
            {
                await userService.DeleteUsers(userModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
