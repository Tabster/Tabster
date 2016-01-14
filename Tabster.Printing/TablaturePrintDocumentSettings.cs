namespace Tabster.Printing
{
    public sealed class TablaturePrintDocumentSettings : TablaturePrintDocumentSettingsBase
    {
        public string Title { get; set; }

        public bool DisplayTitle { get; set; }
        public bool DisplayPrintTime { get; set; }
    }
}