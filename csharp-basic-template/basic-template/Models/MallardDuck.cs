using template.Models.algorithms.implementations;
using template.Models.algorithms.interfaces;
using System;

namespace template.Models
{
    public class MallardDuck : Duck
    {
        public MallardDuck(IQuackBehaviour quack,
            IFlyBehaviour fly)
        {
            quackBehaviour = quack;
            flyBehaviour = fly;
        }

        public override void Display()
        {
            Console.WriteLine("Looks like a Mallard Duck");
        }
    }
}
