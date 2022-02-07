using csharp_net_core_3._1_DI.entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_net_core_3._1_DI.services
{
    public class VehicleService : IVehicleService
    {
        public Vehicle GetVehicle()
        {
            // get from DDBB
            return new Vehicle();
        }

    }
}
