using template.Models.algorithms.interfaces;
using System;

namespace template.Models.algorithms.implementations
{
    public class FlyNoWay : IFlyBehaviour
    {
        public void Fly()
        {
            Console.WriteLine("this duck cannot fly");
        }

    }
}
