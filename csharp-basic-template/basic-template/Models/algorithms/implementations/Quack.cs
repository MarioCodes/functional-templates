using template.Models.algorithms.interfaces;
using System;

namespace template.Models.algorithms.implementations
{
    public class Quack : IQuackBehaviour
    {
        void IQuackBehaviour.Quack()
        {
            Console.WriteLine("quacks like a duck");
        }

    }
}
