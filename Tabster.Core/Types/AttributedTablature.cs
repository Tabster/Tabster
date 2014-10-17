namespace Tabster.Core.Types
{
    public class AttributedTablature : ITablature, ITablatureAttributes
    {
        public AttributedTablature()
        {
        }

        public AttributedTablature(string artist, string title, TablatureType type)
        {
            Artist = artist;
            Title = title;
            Type = type;
        }

        public AttributedTablature(ITablatureAttributes attributes, ITablature tablature)
            : this(attributes.Artist, attributes.Title, attributes.Type)
        {
            Contents = tablature.Contents;
        }

        #region Implementation of ITablatureAttributes

        public string Artist { get; set; }

        public string Title { get; set; }

        public TablatureType Type { get; set; }

        #endregion

        #region Implementation of ITablature

        public string Contents { get; set; }

        #endregion
    }
}