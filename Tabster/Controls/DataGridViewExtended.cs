#region

using System.Windows.Forms;

#endregion

namespace Tabster.Controls
{
    public class DataGridViewExtended : DataGridView
    {
        protected override void InitLayout()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.InitLayout();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            var key = (keyData & Keys.KeyCode);
            return key == Keys.Enter ? ProcessRightKey(keyData) : base.ProcessDialogKey(keyData);
        }

        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            return e.KeyCode == Keys.Enter ? ProcessRightKey(e.KeyData) : base.ProcessDataGridViewKey(e);
        }

        public DataGridViewColumn GetColumnByHeaderText(string text)
        {
            foreach (DataGridViewColumn column in Columns)
            {
                if (column.HeaderText == text)
                    return column;
            }

            return null;
        }
    }
}