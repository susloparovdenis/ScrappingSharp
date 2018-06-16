using FluentAssertions;
using ScrappingSharp.Engines;
using Xunit;

namespace ScrappingSharp.Tests.Engines
{
    public class GoogleTests
    {
        [Fact]
        public async void GoogleScrapper_ReturnsNotEmptyResult()
        {
            var googleScrapper = new Google();
            var result = await googleScrapper.Scrap("go language learning");
            result.Should().NotBeNullOrEmpty();
        }
    }
}