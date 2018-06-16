using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ScrappingSharp;
using ScrappingSharp.Engines;

namespace ConsoleTest
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var query = args.ElementAtOrDefault(1) ?? "concurrency wiki";
            var aggregator = new Aggregator(new Google(), new Yahoo());
            var result = await aggregator.Scrap(query);
            var json = JsonConvert.SerializeObject(result, Formatting.Indented);
            Console.Out.WriteLine(json);
        }
    }
}