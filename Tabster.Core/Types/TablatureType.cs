#region

using System;
using System.Collections.Generic;

#endregion

namespace Tabster.Core.Types
{
    /// <summary>
    /// Represents a type of tablature.
    /// </summary>
    public sealed class TablatureType
    {
        /// <summary>
        /// Initializes a new TablatureType instance.
        /// </summary>
        /// <param name="name"></param>
        public TablatureType(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The name of the tablature type.
        /// </summary>
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

        public static TablatureType Guitar { get; private set; }

        public static TablatureType Chords { get; private set; }

        public static TablatureType Bass { get; private set; }

        public static TablatureType Drum { get; private set; }

        public static TablatureType Ukulele { get; private set; }

        #endregion

        #region Static Methods

        private static readonly List<TablatureType> NativeTypes = new List<TablatureType>();
        private static readonly List<TablatureType> KnownTypes = new List<TablatureType>();

        static TablatureType()
        {
            Guitar = new TablatureType("Guitar");
            Chords = new TablatureType("Chords");
            Bass = new TablatureType("Bass");
            Drum = new TablatureType("Drum");
            Ukulele = new TablatureType("Ukulele");

            NativeTypes = new List<TablatureType> {Guitar, Chords, Bass, Drum, Ukulele};
            KnownTypes.AddRange(NativeTypes);
        }

        public static TablatureType[] GetKnownTypes()
        {
            return KnownTypes.ToArray();
        }

        public static TablatureType[] GetNativeTypes()
        {
            return NativeTypes.ToArray();
        }

        public static void RegisterType(TablatureType type)
        {
            if (!IsRegistered(type))
                KnownTypes.Add(type);
        }

        public static void UnregisterType(TablatureType type)
        {
            KnownTypes.Remove(type);
        }

        public static bool IsRegistered(TablatureType type)
        {
            return KnownTypes.Contains(type);
        }

        public static TablatureType GetTypeByName(string name, bool createIfMissing = false)
        {
            var match = KnownTypes.Find(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (match != null)
            {
                return match;
            }

            if (createIfMissing)
            {
                var type = new TablatureType(name);
                RegisterType(type);
                return type;
            }

            return null;
        }

        #endregion
    }
}