using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using testingZone.Models;

namespace testingZone.Feature
{
    public class TapProgramming
    {
        private const string URL = "https://myfakeapi.com/api/cars/";
        private const int PARALLEL_LIMIT = 10;

        private readonly HttpClient _httpClient = new HttpClient();
        
        List<string> _idsList = Enumerable.Range(1, 50)
            .Select(n => n.ToString())
            .ToList();

        public async Task Start()
        {
            var normal = await Control();
            var task = await TaskWhenAll();
            var parallel = await ParallelForEachAsync();

            Console.WriteLine($"normal version contents '{JsonConvert.SerializeObject(normal)}' \n");
            Console.WriteLine($"task version contents '{JsonConvert.SerializeObject(task)}' \n");
            Console.WriteLine($"parallel version contents '{JsonConvert.SerializeObject(parallel)}'");
        }
        
        // normal version - a task launched and awaited one at a time
        private async Task<List<CarResponse>> Control()
        {
            var stopwatch = Stopwatch.StartNew();
            List<CarResponse> carList = [];
            foreach (string id in _idsList)
            {
                CarResponse singleCar = await ExecuteCall(id);
                carList.Add(singleCar);
            }
            Console.WriteLine($"normal version completed at '{stopwatch.ElapsedMilliseconds}' ms");
            stopwatch.Stop();
            return carList;
        }

        // taskWhenAll version - all tasks launched at the same time
        // most simple one. ideal for when there's no limits as we have no control over the number of calls at a time
        private async Task<List<CarResponse>> TaskWhenAll()
        {
            var stopwatch = Stopwatch.StartNew();

            var getCarsTask = _idsList.Select(ExecuteCall);
            var cars = await Task.WhenAll(getCarsTask);

            Console.WriteLine($"Task.WhenAll() version completed at '{stopwatch.ElapsedMilliseconds}' ms");
            stopwatch.Stop();
            return cars.ToList();
        }

        // .NET6+ but it gives us control over the number of parallel calls but it has more complexity
        private async Task<List<CarResponse>> ParallelForEachAsync()
        {
            var stopwatch = Stopwatch.StartNew();

            // this is a secure collection for multiple threads
            var dtosBag = new ConcurrentBag<CarResponse>();
            var options = new ParallelOptions { MaxDegreeOfParallelism = PARALLEL_LIMIT };

            await Parallel.ForEachAsync(_idsList, options, async (id, ct) =>
            {
                CarResponse singleCar = await ExecuteCall(id);
                dtosBag.Add(singleCar);
            });
            Console.WriteLine($"Parallel.ForEachAsync() version with limit at '{PARALLEL_LIMIT}' completed at '{stopwatch.ElapsedMilliseconds}' ms \n");
            stopwatch.Stop();

            return dtosBag.ToList();
        }

        private async Task<CarResponse> ExecuteCall(string id)
        {
            string combinedUrl = URL + id;

            using var response = await _httpClient.GetAsync(combinedUrl);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CarResponse>(json);
        }
    }
}
