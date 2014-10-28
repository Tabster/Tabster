#region

using System;
using HtmlAgilityPack;
using Tabster.Core.Types;
using Tabster.Data;
using Tabster.Data.Processing;

#endregion

namespace GuitartabsDotCC
{
    internal class GuitartabsDotCCParser : ITablatureWebpageImporter
    {
        #region Implementation of ITablatureWebpageImporter

        public string SiteName
        {
            get { return "Guitartabs.cc"; }
        }

        public TablatureDocument Parse(string text, TablatureType type)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(text);

            string contents = null;

            var pageTitle = doc.DocumentNode.SelectSingleNode("//head/title").InnerText;
            var titleSplit = pageTitle.Split(new[] {" - "}, StringSplitOptions.None);

            if (titleSplit.Length < 2)
            {
                return null;
            }

            var artist = titleSplit[0];
            var title = titleSplit[1];

            var container = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'commonbox')]");

            if (container != null)
            {
                var infoTable = container.SelectSingleNode("//table[contains(@class, 'tabslist')]");

                if (infoTable != null)
                {
                    var infoCells = infoTable.SelectNodes("tr//th");

                    //get tab type
                    if (type == null)
                    {
                        if (infoCells != null && infoCells.Count > 2)
                        {
                            var typeCell = infoCells[2].SelectSingleNode("strong");

                            switch (typeCell.InnerText)
                            {
                                case "Tab":
                                    type = TablatureType.Guitar;
                                    break;
                                case "Chords":
                                    type = TablatureType.Chords;
                                    break;
                                case "Bass Tab":
                                    type = TablatureType.Bass;
                                    break;
                                case "Drum Tab":
                                    type = TablatureType.Drum;
                                    break;
                            }
                        }
                    }
                }

                var contentsNode = container.SelectSingleNode("//div[@class='tabcont']/div[4]/font/pre");

                if (contentsNode != null)
                {
                    //embedded hyperlink
                    var span = contentsNode.SelectSingleNode("span");

                    if (span != null)
                    {
                        contentsNode.RemoveChild(span);
                    }

                    contents = contentsNode.InnerText;
                }
            }

            if (type == null || artist == null || title == null || contents == null)
                return null;

            return new TablatureDocument(artist, title, type, contents);
        }

        public bool MatchesUrlPattern(Uri url)
        {
            return url.IsWellFormedOriginalString() && (url.DnsSafeHost == "guitartabs.cc" || url.DnsSafeHost == "www.guitartabs.cc");
        }

        #endregion
    }
}