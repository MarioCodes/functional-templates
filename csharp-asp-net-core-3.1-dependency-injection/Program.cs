using template.Models;
using template.Models.algorithms.implementations;

namespace template
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Duck duck = new MallardDuck(new Quack(), new FlyNoWay());
            duck.PerformQuack();
            duck.PerformFly();
            duck.Display();
        }

    }
}
