#region

using System.Windows.Forms;

#endregion

namespace PngFile
{
    public partial class FontSizeDialog : Form
    {
        public FontSizeDialog()
        {
            InitializeComponent();
        }

        public int FontSize
        {
            get { return (int) numericUpDown1.Value; }
        }
    }
}