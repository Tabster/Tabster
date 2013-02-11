#region

using System;
using System.Windows.Forms;
using Tabster.Properties;

#endregion

namespace Tabster.Forms
{
    partial class Form1
    {
        private bool _firstBrowserLoad; //stores value indicating whether the user has already loaded the browser


        private void saveTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            Global.Tabs.Save(webBrowser1.Url.ToString());
            LoadLibrary(true);*/
        }

        private void ChangeNavigationControl(bool showRefresh)
        {
            navigationButton.Image = showRefresh ? Resources.arrow_refresh : Resources.stop;
            //refreshToolStripMenuItem.Text = showRefresh ? "Refresh" : "Stop";
        }

        private void History_Navigation(object sender, EventArgs e)
        {
            var url = ((ToolStripMenuItem)sender).ToolTipText;
            webBrowser1.Navigate(url);
        }

        private void HideCrap()
        {
            var htmlDocument = webBrowser1.Document;

            if (htmlDocument != null)
            {
                // Hide all iframes
                /*
                var iframe = htmlDocument.GetElementById("iframe");

                if (iframe != null && iframe.GetAttribute("src").Contains("ad"))
                {
                    //Console.WriteLine(iframe.InnerHtml);
                    htmlDocument.GetElementById("iframe").SetAttribute("width", "0");
                    htmlDocument.GetElementById("iframe").SetAttribute("height", "0");
                }*/

                /*
                var bannerRemoval = GetElementByClass("a", "removeaddlink");

                if (bannerRemoval != null)
                    bannerRemoval.InnerText = "";
                */

                //hide tab pro

                /*
                var tab_pro_controls = GetElementByClass("div", "p_h");

                Console.WriteLine("test");

                if (tab_pro_controls != null)
                {
                    Console.WriteLine("tab_pro_controls found");
                    tab_pro_controls.InnerHtml = "";
                }*/
            }
        }

        private HtmlElement GetElementByClass(string tagName, string className)
        {
            if (webBrowser1.Document != null)
            {
                var elems = webBrowser1.Document.GetElementsByTagName(tagName);

                foreach (HtmlElement elem in elems)
                {
                    if (elem.GetAttribute("class") == className)
                    {
                        return elem;
                    }
                }
            }

            return null;
        }

        #region WebBrowser Events

        private void webBrowser1_CanGoBackChanged(object sender, EventArgs e)
        {
            goBackButton.Enabled = webBrowser1.CanGoBack;
            //backToolStripMenuItem.Enabled = webBrowser1.CanGoBack;
        }

        private void webBrowser1_CanGoForwardChanged(object sender, EventArgs e)
        {
            goForwardButton.Enabled = webBrowser1.CanGoForward;
            //forwardToolStripMenuItem.Enabled = webBrowser1.CanGoForward;
        }

        private void webBrowser1_NewWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            ChangeNavigationControl(false);
            loadingIndicator.Visible = true;

        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            //IsValidUltimateGuitarTabURL

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            loadingIndicator.Visible = false;

            if (e.Url.Host.Contains("ultimate-guitar.com"))
            {
                addressBar.Text = e.Url.ToString();
                ChangeNavigationControl(true);
                loadingIndicator.Visible = false;

                /*
                if (goBackButton.DropDownItems.Count == HistoryItems)
                {
                    goBackButton.DropDownItems.RemoveAt(goBackButton.DropDownItems.Count - 1);
                }

                var tsi = new ToolStripMenuItem(webBrowser1.DocumentTitle) {ToolTipText = e.Url.ToString()};
                tsi.Click += (History_Navigation);
                goBackButton.DropDownItems.Add(tsi);*/

                //HideCrap();
            }
        }

        #endregion

        #region Toolbar

        private void goBackButton_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void goForwardButton_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void navigationButton_Click(object sender, EventArgs e)
        {
            if (webBrowser1.IsBusy)
            {
                webBrowser1.Stop();
            }

            else
            {
                webBrowser1.Refresh();
            }
        }

        #endregion
    }
}