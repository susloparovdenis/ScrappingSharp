using FluentAssertions;
using ScrappingSharp.Engines;
using Xunit;

namespace ScrappingSharp.Tests.Engines
{
    public class YahooTests
    {
        [Fact]
        public async void YahooScrapper_ReturnsNotEmptyResult()
        {
            var yahooScrapper = new Yahoo();
            var result = await yahooScrapper.Scrap("concurrency wiki");
            result.Should().NotBeNullOrEmpty();
        }
    }
}