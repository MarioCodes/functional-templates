using csharp_net_core_3._1_DI.entity;
using csharp_net_core_3._1_DI.services;
using System;

namespace csharp_net_core_3._1_DI.consumer
{
    public class VehicleConsumer
    {
        private readonly IVehicleService _vehicleService;

        public VehicleConsumer(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public void Consume()
        {
            Vehicle vehicle = _vehicleService.GetVehicle();
            Console.WriteLine(vehicle.ToString());
        }
    }
}
