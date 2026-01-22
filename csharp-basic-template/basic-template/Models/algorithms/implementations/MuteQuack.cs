using template.Models.algorithms.interfaces;
using System;

namespace template.Models.algorithms.implementations
{
    public class MuteQuack : IQuackBehaviour
    {
        public void Quack()
        {
            Console.WriteLine("<<Silence>>");
        }

    }
}
