#region

using System;
using HtmlAgilityPack;
using Tabster.Core.FileTypes;
using Tabster.Core.Plugins;
using Tabster.Core.Types;

#endregion

namespace GuitartabsDotCC
{
    internal class GuitartabsDotCCParser : ITabParser
    {
        #region Implementation of ITabParser

        public string Name
        {
            get { return "Guitartabs.cc"; }
        }

        public Version Version
        {
            get { return new Version("1.0"); }
        }

        public TablatureDocument ParseTabFromSource(string source, TabType? type)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(source);

            var tabType = type;
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
                    if (!tabType.HasValue)
                    {
                        if (infoCells != null && infoCells.Count > 2)
                        {
                            var typeCell = infoCells[2].SelectSingleNode("strong");

                            switch (typeCell.InnerText)
                            {
                                case "Tab":
                                    tabType = TabType.Guitar;
                                    break;
                                case "Chords":
                                    tabType = TabType.Chords;
                                    break;
                                case "Bass Tab":
                                    tabType = TabType.Bass;
                                    break;
                                case "Drum Tab":
                                    tabType = TabType.Drum;
                                    break;
                            }
                        }
                    }
                }

                var contentsNode = container.SelectSingleNode("//div[@class='tabcont']/div[4]/font/pre");

                if (contentsNode != null)
                {
                    var span = contentsNode.SelectSingleNode("span");

                    if (span != null)
                    {
                        contentsNode.RemoveChild(span);
                    }

                    contents = contentsNode.InnerText;
                }
            }

            if (!tabType.HasValue || artist == null || title == null || contents == null)
                return null;

            return new TablatureDocument(artist, title, tabType.Value, contents);
        }

        public bool MatchesUrlPattern(Uri url)
        {
            return url.IsWellFormedOriginalString() && (url.DnsSafeHost == "guitartabs.cc" || url.DnsSafeHost == "www.guitartabs.cc");
        }

        #endregion
    }
}