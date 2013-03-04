#region

using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Tabster.Controls
{
    public class DataGridViewExtended : DataGridView
    {
        public bool TransparentColumns { get; set; }
        public bool TransparentRows { get; set; }

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

        protected override void PaintBackground(Graphics graphics, Rectangle clipBounds, Rectangle gridBounds)
        {
            base.PaintBackground(graphics, clipBounds, gridBounds);

            return;

            var rectSource = new Rectangle(Location.X, Location.Y, Width, Height);
            var rectDest = new Rectangle(0, 0, rectSource.Width, rectSource.Height);

            var b = new Bitmap(Parent.ClientRectangle.Width, Parent.ClientRectangle.Height);
            Graphics.FromImage(b).DrawImage(Parent.BackgroundImage, Parent.ClientRectangle);

            graphics.DrawImage(b, rectDest, rectSource, GraphicsUnit.Pixel);

            if (TransparentColumns)
            {
                EnableHeadersVisualStyles = false;
                ColumnHeadersDefaultCellStyle.BackColor = Color.Transparent;
                RowHeadersDefaultCellStyle.BackColor = Color.Transparent;

                foreach (DataGridViewColumn col in Columns)
                {
                    col.DefaultCellStyle.BackColor = Color.Transparent;
                    col.DefaultCellStyle.SelectionBackColor = Color.Transparent;
                }
            }

            if (TransparentRows)
            {
                //EnableHeadersVisualStyles = false;
                //ColumnHeadersDefaultCellStyle.BackColor = Color.Transparent;
                //RowHeadersDefaultCellStyle.BackColor = Color.Transparent;

                foreach (DataGridViewRow row in Rows)
                {
                    GridColor = Color.Transparent;
                    row.DefaultCellStyle.BackColor = Color.Transparent;
                    row.DefaultCellStyle.SelectionBackColor = Color.Transparent;
                }
            }
        }
    }
}