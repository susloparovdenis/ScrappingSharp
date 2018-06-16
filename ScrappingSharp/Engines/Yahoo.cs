using AngleSharp.Dom;
using ScrappingSharp.Abstractions;

namespace ScrappingSharp.Engines
{
    public class Yahoo : FirstPageScrapperBase
    {
        protected override string Url { get; } = "https://search.yahoo.com/search?p={0}";
        protected override string EntrySelector { get; } = ".algo";
        protected override string GetTitle(IElement element) => element.QuerySelector(".title > a").TextContent;
        protected override string GetUrl(IElement element) => element.QuerySelector("a.ac-algo").Attributes["href"].Value;
    }
}