using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using ScrappingSharp.Abstractions;
using Xunit;

namespace ScrappingSharp.Tests
{
    public class AggregatorTests
    {
        private static ISearchEngineScrapper CreateEngineStub(params Result[] results)
        {
            var scrapper = A.Fake<ISearchEngineScrapper>();
            A.CallTo(() => scrapper.Scrap(A<string>._))
                .Returns(results);
            return scrapper;
        }
        
        [Fact]
        public async Task Aggregator_NotCombinesDifferentUrls()
        {
            var scrapper1 = CreateEngineStub(new Result("title", "url", "google"));
            var scrapper2 = CreateEngineStub(new Result("title", "url2", "yahoo"));
            var aggregator = new Aggregator(scrapper1, scrapper2);
            
            var result = await aggregator.Scrap("");
            
            result.Should().HaveCount(2).And.OnlyContain(r => r.Sources.Count == 1);
        }
        
        [Fact]
        public async Task Aggregator_CombinesCorrectly()
        {
            var scrapper1 = CreateEngineStub(new Result("title", "url", "google"));
            var scrapper2 = CreateEngineStub(new Result("title", "url", "yahoo"));
            var aggregator = new Aggregator(scrapper1, scrapper2);
            
            var result = await aggregator.Scrap("");
            
            result.Should().ContainSingle(r => r.Sources.Count == 2);
        }


    }
}