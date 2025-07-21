using template.Models.algorithms.implementations;
using System;

namespace template.Models
{
    public class RedheadDuck : Duck
    {
        public RedheadDuck()
        {
            quackBehaviour = new Quack();
            flyBehaviour = new FlyWithWings();
        }

        public override void Display()
        {
            Console.WriteLine("Looks like a Redhead Duck");
        }
    }
}
