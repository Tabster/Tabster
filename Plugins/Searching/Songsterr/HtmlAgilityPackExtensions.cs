#region

using System.Text;
using HtmlAgilityPack;

#endregion

namespace Songsterr
{
    internal static class HtmlAgilityPackExtensions
    {
        public static string GetExclusiveInnerText(this HtmlNode node, bool trimInnerText)
        {
            var textNodes = node.SelectNodes("text()");

            var sb = new StringBuilder();

            foreach (var child in textNodes)
            {
                var innerText = trimInnerText ? child.InnerText.Trim() : child.InnerText;

                if (!string.IsNullOrEmpty(innerText))
                    sb.Append(innerText);
            }

            return sb.ToString();
        }
    }
}