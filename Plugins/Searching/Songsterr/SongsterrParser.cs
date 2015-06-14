#region

using System;
using System.Net;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using Tabster.Core.Types;
using Tabster.Data.Processing;

#endregion

namespace Songsterr
{
    public class SongsterrParser : ITablatureWebImporter
    {
        #region Implementation of ITablatureWebImporter

        public string SiteName
        {
            get { return "Songsterr"; }
        }

        public Uri Homepage { get; private set; }

        public Version Version
        {
            get { return new Version("1.0"); }
        }

        public bool IsUrlParsable(Uri url)
        {
            return url.IsWellFormedOriginalString() && ((url.DnsSafeHost.EndsWith("songsterr.com", StringComparison.InvariantCultureIgnoreCase) && url.ToString().Contains("/a/wsa/")));
        }

        public AttributedTablature Parse(Uri url, WebProxy proxy)
        {
            string html;

            using (var client = new WebClient {Proxy = proxy})
            {
                html = client.DownloadString(url);
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var isTextVersion = doc.DocumentNode.SelectSingleNode("//head/title").InnerText.Contains("text version");

            var textButton = doc.DocumentNode.SelectSingleNode("//a[@class='btn active' and @title='text version']");

            //just incase text indicator is removed for title tag
            if (!isTextVersion && textButton != null)
                isTextVersion = true;

            if (!isTextVersion)
                throw new TablatureProcessorException("Not a text-based tab.");

            var titleHeader = doc.DocumentNode.SelectSingleNode("//h1[@class='inlineHeading']");

            var tabArtist = titleHeader.SelectSingleNode("a").InnerText;
            var tabTitle = titleHeader.GetExclusiveInnerText(true).Substring("- ".Length);
            TablatureType tabType = null;

            if (textButton != null)
            {
                var tabTypeFromUrl = Common.GetTabTypeFromURL(new Uri(string.Format("http://songsterr.com/{0}", textButton.Attributes["href"].Value)));
                if (tabTypeFromUrl != null)
                    tabType = tabTypeFromUrl;
            }

            if (tabType == null)
                return null;

            var containerDiv = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'text-tab-wrapper')]");

            var pre = containerDiv.SelectSingleNode("pre");

            pre.FirstChild.Remove();
            pre.FirstChild.Remove();

            var tabBuilder = new StringBuilder();

            var lastNodeTabFragment = false;

            foreach (var child in pre.ChildNodes)
            {
                if (child.NodeType == HtmlNodeType.Text)
                {
                    var innerText = HttpUtility.HtmlDecode(child.InnerText);

                    //don't trim lyrics
                    if (!lastNodeTabFragment)
                    {
                        innerText = innerText.TrimStart();
                    }

                    tabBuilder.Append(innerText);

                    lastNodeTabFragment = false;
                }

                else if (child.Name == "table" && child.HasAttributes && child.Attributes["class"].Value == "text-tab-fragment")
                {
                    var parsed = ParseTabFragment(child);

                    tabBuilder.Append(parsed);
                    lastNodeTabFragment = true;
                }

                else if (child.Name == "span")
                {
                    if (child.HasAttributes && child.Attributes["class"].Value == "chord")
                    {
                        //todo something
                    }
                }

                else
                {
                    tabBuilder.Append(HttpUtility.HtmlDecode(child.InnerText).TrimStart());
                    tabBuilder.Append(Environment.NewLine);

                    //header
                    if (child.Name.Length == 2 && child.Name[0] == 'h')
                    {
                        tabBuilder.Append(Environment.NewLine);
                    }

                    lastNodeTabFragment = false;
                }
            }

            var contents = tabBuilder.ToString().Replace("\n", Environment.NewLine);

            return new AttributedTablature(tabArtist, tabTitle, tabType) {Contents = contents};
        }

        #endregion

        private static string ParseTabFragment(HtmlNode node)
        {
            var fragmentBuilder = new StringBuilder();

            const int LINE_LENGTH = 72;
            const int MEASURE_LENGTH = 17;

            var lineNodes = node.SelectNodes("tr");

            var lineBuilder = new StringBuilder(LINE_LENGTH);

            foreach (var lineNode in lineNodes)
            {
                var line = lineNode.InnerText;

                lineBuilder.Append("|");

                for (var i = 0; i < line.Length; i++)
                {
                    var c = line[i];

                    if (char.IsWhiteSpace(c.ToString(), 0))
                    {
                        lineBuilder.Append("-");
                    }

                    else
                    {
                        lineBuilder.Append(c);
                    }

                    if (i > 0 && i < 68 && i%MEASURE_LENGTH == 0)
                    {
                        lineBuilder.Append("*");
                    }
                }

                lineBuilder.Append("|");

                fragmentBuilder.AppendLine(lineBuilder.ToString());

                lineBuilder.Length = 0;
            }

            return fragmentBuilder.ToString();
        }
    }
}