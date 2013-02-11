#region

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HtmlAgilityPack;

#endregion

namespace Tabster.UltimateGuitar
{
    public class Top100
    {
        private const string _urlall = "http://www.ultimate-guitar.com/top/top100.htm";
        private const string _urltabs = "http://www.ultimate-guitar.com/top/top100_tabs.htm";
        private const string _urlchords = "http://www.ultimate-guitar.com/top/top100_chords.htm";
        private const string _urlbass = "http://www.ultimate-guitar.com/top/top100_bass.htm";
        private const string _urlpro = "http://www.ultimate-guitar.com/top/top100_pro.htm";
        private const string _urlpower = "http://www.ultimate-guitar.com/top/top100_power.htm";
        private const string _urlvideo = "http://www.ultimate-guitar.com/top/top100_video.htm";
        private const string _urldrum = "http://www.ultimate-guitar.com/top/top100_drum_tab.htm";

        private readonly List<UltimateGuitarTab> _tabs = new List<UltimateGuitarTab>();

        public Top100(UltimateGuitarTabType type = UltimateGuitarTabType.Undefined)
        {
            switch (type)
            {
                case UltimateGuitarTabType.Undefined:
                    URL = new Uri(_urlall);
                    break;
                case UltimateGuitarTabType.Video:
                    URL = new Uri(_urlvideo);
                    break;
                case UltimateGuitarTabType.GuitarTab:
                    URL = new Uri(_urltabs);
                    break;
                case UltimateGuitarTabType.GuitarChords:
                    URL = new Uri(_urlchords);
                    break;
                case UltimateGuitarTabType.BassTab:
                    URL = new Uri(_urlbass);
                    break;
                case UltimateGuitarTabType.GuitarPro:
                    URL = new Uri(_urlpro);
                    break;
                case UltimateGuitarTabType.PowerTab:
                    URL = new Uri(_urlpower);
                    break;
                case UltimateGuitarTabType.DrumTab:
                    URL = new Uri(_urldrum);
                    break;
            }

            string data;

            using (var client = new TabsterWebClient())
            {
                data = client.DownloadString(URL);
            }

            if (data.Length > 0)
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(data);

                //var tresults = doc.DocumentNode.SelectSingleNode("//table[@class='b3']");

                //Console.WriteLine(tresults.InnerHtml);

                var table = doc.DocumentNode.SelectNodes("/html/body/center/div/div[2]/div/center/div/table[2]/tbody/tr/td/table/tbody/tr/td[2]/table/tbody/tr[2]/td/table[2]");

                Console.WriteLine(table.Count);

                _tabs.Clear();
            }
        }

        public Uri URL { get; private set; }

        public ReadOnlyCollection<UltimateGuitarTab> Tabs
        {
            get { return _tabs.AsReadOnly(); }
        }
    }
}