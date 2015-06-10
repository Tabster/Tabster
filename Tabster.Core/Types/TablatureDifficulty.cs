#region

using System;
using System.Collections.Generic;

#endregion

namespace Tabster.Core.Types
{
    public sealed class TablatureDifficulty
    {
        public TablatureDifficulty(string name)
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

        public static bool operator ==(TablatureDifficulty a, TablatureDifficulty b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (((object) a == null) || ((object) b == null))
                return false;

            return a.Name == b.Name;
        }

        public static bool operator !=(TablatureDifficulty x, TablatureDifficulty y)
        {
            return !(x == y);
        }

        public bool Equals(TablatureDifficulty other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (TablatureDifficulty)) return false;
            return Equals((TablatureDifficulty) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        #region Natively Supported Difficulties

        public static TablatureDifficulty Novice { get; private set; }
        public static TablatureDifficulty Intermediate { get; private set; }
        public static TablatureDifficulty Advance { get; private set; }

        #endregion

        #region Static Methods

        private static readonly List<TablatureDifficulty> NativeDifficulties = new List<TablatureDifficulty>();
        private static readonly List<TablatureDifficulty> KnownDifficulties = new List<TablatureDifficulty>();

        static TablatureDifficulty()
        {
            Novice = new TablatureDifficulty("Novice");
            Intermediate = new TablatureDifficulty("Intermediate");
            Advance = new TablatureDifficulty("Advance");

            NativeDifficulties = new List<TablatureDifficulty> {Novice, Intermediate, Advance};
            KnownDifficulties.AddRange(NativeDifficulties);
        }

        public static TablatureDifficulty[] GetKnownDifficulties()
        {
            return KnownDifficulties.ToArray();
        }

        public static TablatureDifficulty[] GetNativeDifficulties()
        {
            return NativeDifficulties.ToArray();
        }

        public static void RegisterDifficulty(TablatureDifficulty difficulty)
        {
            if (!IsRegistered(difficulty))
                KnownDifficulties.Add(difficulty);
        }

        public static void UnregisterDifficulty(TablatureDifficulty difficulty)
        {
            KnownDifficulties.Remove(difficulty);
        }

        public static bool IsRegistered(TablatureDifficulty difficulty)
        {
            return KnownDifficulties.Contains(difficulty);
        }

        public static TablatureDifficulty GetDifficultyByName(string name, bool createIfMissing = false)
        {
            var match = KnownDifficulties.Find(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (match != null)
            {
                return match;
            }

            if (createIfMissing)
            {
                var difficulty = new TablatureDifficulty(name);
                RegisterDifficulty(difficulty);
                return difficulty;
            }

            return null;
        }

        #endregion
    }
}