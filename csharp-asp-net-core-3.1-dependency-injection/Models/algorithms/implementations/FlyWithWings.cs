using template.Models.algorithms.interfaces;
using System;

namespace template.Models.algorithms.implementations
{
    public class FlyWithWings : IFlyBehaviour
    {
        public void Fly()
        {
            Console.WriteLine("Flying with wings");
        }

    }
}
