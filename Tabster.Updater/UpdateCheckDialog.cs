#region

using System;
using System.Windows.Forms;

#endregion

namespace Tabster.Updater
{
    public partial class UpdateCheckDialog : Form
    {
        private readonly UpdateQuery _query;

        public UpdateCheckDialog(UpdateQuery query = null)
        {
            InitializeComponent();
            _query = query;

            if (query == null)
            {
                _query = new UpdateQuery();

                _query.Completed += _query_Completed;
                _query.Check(false);
            }

            else
            {
                UpdateStatus();
            }
        }

        private void _query_Completed(object sender, UpdateQueryCompletedEventArgs e)
        {
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            if (_query != null)
            {
                if (_query.Error != null)
                {
                    lblstatus.Text = "An error occured while checking for updates.";
                }

                else
                {
                    updatebtn.Enabled = _query.UpdateAvailable;
                    lblstatus.Text = string.Format("Tabser {0} is available for download!", _query.Version);
                }
            }
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            Hide();
            var updateDialog = new UpdateDialog(_query);
            updateDialog.Show();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}