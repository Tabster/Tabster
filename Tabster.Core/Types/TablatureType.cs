#region

using System.Collections.Generic;

#endregion

namespace Tabster.Core.Types
{
    public sealed class TablatureType
    {
        public TablatureType(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public override string ToString()
        {
            return Name;
        }

        public string ToFriendlyString()
        {
            return Name;
        }

        public static bool operator ==(TablatureType a, TablatureType b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (((object) a == null) || ((object) b == null))
                return false;

            return a.Name == b.Name;
        }

        public static bool operator !=(TablatureType x, TablatureType y)
        {
            return !(x == y);
        }

        public bool Equals(TablatureType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (TablatureType)) return false;
            return Equals((TablatureType) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        #region Natively Supported Types

        public static TablatureType Guitar
        {
            get { return new TablatureType("Guitar"); }
        }

        public static TablatureType Chords
        {
            get { return new TablatureType("Chords"); }
        }

        public static TablatureType Bass
        {
            get { return new TablatureType("Bass"); }
        }

        public static TablatureType Drum
        {
            get { return new TablatureType("Drum"); }
        }

        public static TablatureType Ukulele
        {
            get { return new TablatureType("Ukulele"); }
        }

        #endregion

        #region Static Methods

        private static readonly List<TablatureType> _knownTypes = new List<TablatureType>();

        static TablatureType()
        {
            _knownTypes = new List<TablatureType> {Guitar, Chords, Bass, Drum, Ukulele};
        }

        public static List<TablatureType> GetKnownTypes()
        {
            return _knownTypes;
        }

        public static void RegisterType(TablatureType type)
        {
            _knownTypes.Add(type);
        }

        public static void UnregisterType(TablatureType type)
        {
            _knownTypes.Remove(type);
        }

        #endregion
    }
}