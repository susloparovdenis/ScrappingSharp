using AngleSharp.Dom;
using ScrappingSharp.Abstractions;

namespace ScrappingSharp.Engines
{
    public class Google : FirstPageScrapperBase
    {
        protected override string Url { get; } = "https://www.google.com/search?q={0}";
        protected override string EntrySelector { get; } = ".g";
        
        protected override string GetTitle(IElement element) => element.QuerySelector(".r>a").TextContent;

        protected override string GetUrl(IElement element) => element.QuerySelector("cite").TextContent;
    } 
}