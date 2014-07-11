namespace Tabster.Core
{
    public class Playlist : TabFileCollection
    {
        public Playlist(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}