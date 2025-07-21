using System;
using testingZone.Feature;

namespace template
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PassByValueAndReference service = new();
            service.Start();
        }

    }
}
