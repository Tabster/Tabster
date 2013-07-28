#region

using System;
using System.Windows.Forms;

#endregion

namespace Tabster.Forms
{
    public partial class TabDetailsDialog : Form
    {
        private readonly TabFile _tabFile;

        public TabDetailsDialog(TabFile tab)
        {
            InitializeComponent();
            _tabFile = tab;
            txttype.DataSource = Tab.TabTypes;
            LoadData();
        }

        private void LoadData()
        {
            txtlocation.Text = _tabFile.FileInfo.FullName;

            txtartist.Text = _tabFile.TabData.Artist;
            txtsong.Text = _tabFile.TabData.Title;
            txttype.Text = Tab.GetTabString(_tabFile.TabData.Type);
            txtcomment.Text = _tabFile.TabData.Comment;

            lblFormat.Text += _tabFile.FileVersion;
            lblLength.Text += string.Format(" {0:n0} bytes", _tabFile.FileInfo.Length);
            lblCreated.Text += string.Format(" {0}", _tabFile.FileInfo.CreationTime);
            lblModified.Text += string.Format(" {0}", _tabFile.FileInfo.LastWriteTime);

            var favorited = Program.libraryManager.FindTab(_tabFile).Favorited;
            lblfavorited.Text = string.Format("Favorited: {0}", (favorited ? "Yes" : "No"));

            var playlistCount = Program.libraryManager.FindPlaylistsContaining(_tabFile).Count;
            lblPlaylistCount.Text = string.Format("Founds in {0} playlist{1}.", playlistCount, playlistCount == 1 ? "" : "s");
           
            txtlyrics.Text = _tabFile.TabData.Lyrics;
            txtaudio.Text = _tabFile.TabData.Audio;

            if (!string.IsNullOrEmpty(_tabFile.TabData.Audio))
            {
                axWindowsMediaPlayer1.Visible = true;
                axWindowsMediaPlayer1.URL = txtaudio.Text;
                axWindowsMediaPlayer1.Ctlcontrols.stop();
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            Close();
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            _tabFile.TabData.Artist = txtartist.Text;
            _tabFile.TabData.Title = txtsong.Text;
            _tabFile.TabData.Type = Tab.GetTabType(txttype.Text);
            _tabFile.TabData.Lyrics = txtlyrics.Text;
            _tabFile.TabData.Audio = txtaudio.Text;
            _tabFile.TabData.Comment = txtcomment.Text;
            _tabFile.Save();
        }

        private void browseaudiobtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ofd = new OpenFileDialog
                                     {
                                         Title = "Open Audio File - Tabster",
                                         AddExtension = true,
                                         Multiselect = false,
                                         Filter = "Audio Files (*.mp3)|*.mp3"
                                     })
                {

                    if (ofd.ShowDialog() != DialogResult.Cancel)
                    {
                        txtaudio.Text = ofd.FileName;
                        axWindowsMediaPlayer1.Visible = true;
                        axWindowsMediaPlayer1.URL = ofd.FileName;
                    }
                }
            }

            catch
            {
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}