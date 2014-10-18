namespace Tabster.Controls
{
    public interface ITablatureTextEditor
    {
        bool ReadOnly { get; set; }
        bool Modified { get; set; }
    }
}