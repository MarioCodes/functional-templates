using csharp_net_core_3._1_DI.entity;
using csharp_net_core_3._1_DI.services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_net_core_3._1_DI.controllers
{
    // test it with:
    // http://localhost:18796/api/vehicleController/example
    // https://localhost:35209/api/vehicleController/example

    [Route("api/vehicleController/")]
    [ApiController]
    public class VehicleController: ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        [Route("example")]
        public ActionResult<string> Get()
        {
            Vehicle vehicle = _vehicleService.GetVehicle();
            return vehicle.ToString();
        }

        // the following are usage examples. 

        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

    }
}
