using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScrappingSharp.Abstractions;

namespace ScrappingSharp
{
    public class Aggregator : ISearchEngineScrapper
    {
        private readonly List<ISearchEngineScrapper> _scrappers;

        public Aggregator(params ISearchEngineScrapper[] scrappers)
        {
            if (scrappers.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(scrappers));
            _scrappers = scrappers.ToList();
        }


        public async Task<IList<Result>> Scrap(string query)
        {
            var tasks = _scrappers.Select(s => s.Scrap(query)).ToArray();
            var taskResults = await Task.WhenAll(tasks);
            return CombineResults(taskResults.SelectMany(t => t));
        }

        private IList<Result> CombineResults(IEnumerable<Result> results)
        {
            return results.GroupBy(r => r.Url)
                .Select(grouping => grouping.ToList())
                .Select(MergeSameUrlResults).ToList();
        }

        private Result MergeSameUrlResults(List<Result> results)
        {
            var first = results[0];
            var restSources = results.Skip(1)
                .SelectMany(r => r.Sources);
            first.AddSources(restSources);
            return first;
        }
    }
}