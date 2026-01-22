using template.Models.algorithms.interfaces;
using System;

namespace template.Models
{
    public class Duck
    {
        protected IFlyBehaviour flyBehaviour;
        protected IQuackBehaviour quackBehaviour;

        public void PerformQuack()
        {
            quackBehaviour.Quack();
        }

        public void PerformFly()
        {
            flyBehaviour.Fly();
        }

        public void Swim()
        {
            Console.WriteLine("Swims like a base duck");
        }

        public virtual void Display()
        {
            Console.WriteLine("Looks like a base duck");
        }
    }
}
