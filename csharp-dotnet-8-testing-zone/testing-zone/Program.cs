using System;
using System.Threading.Tasks;
using testingZone.Feature;

namespace template
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            TapProgramming service = new();
            await service.Start();
        }

    }
}
