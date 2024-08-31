using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using testFeatures.Services;

namespace testFeatures.Controllers
{
    [ApiController]
    [Route("testing")]
    public class TestController : ControllerBase
    {

        [HttpGet("async")]
        [SwaggerOperation(Tags = new[] { "tests" })]
        public async Task<IActionResult> AsyncTest([FromServices] ITestService service)
        {
            try
            {
                var result = await service.AsyncTest();
                return Ok(result);
            } catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
