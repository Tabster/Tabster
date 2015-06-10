#region

using System;
using System.Collections.Generic;

#endregion

namespace Tabster.Core.Types
{
    public sealed class TablatureTuning
    {
        public TablatureTuning(string name)
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

        public static bool operator ==(TablatureTuning a, TablatureTuning b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (((object) a == null) || ((object) b == null))
                return false;

            return a.Name == b.Name;
        }

        public static bool operator !=(TablatureTuning x, TablatureTuning y)
        {
            return !(x == y);
        }

        public bool Equals(TablatureTuning other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (TablatureTuning)) return false;
            return Equals((TablatureTuning) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        #region Natively Supported Tunings

        public static TablatureTuning Standard { get; private set; }
        public static TablatureTuning HalfStepDown { get; private set; }
        public static TablatureTuning Btuning { get; private set; }
        public static TablatureTuning Ctuning { get; private set; }
        public static TablatureTuning Dtuning { get; private set; }
        public static TablatureTuning DropA { get; private set; }
        public static TablatureTuning DropASharp { get; private set; }
        public static TablatureTuning DropB { get; private set; }
        public static TablatureTuning DropC { get; private set; }
        public static TablatureTuning DropCSharp { get; private set; }
        public static TablatureTuning DropD { get; private set; }
        public static TablatureTuning OpenC { get; private set; }
        public static TablatureTuning OpenD { get; private set; }
        public static TablatureTuning OpenE { get; private set; }
        public static TablatureTuning OpenG { get; private set; }

        #endregion

        #region Static Methods

        private static readonly List<TablatureTuning> NativeTunings = new List<TablatureTuning>();
        private static readonly List<TablatureTuning> KnownTunings = new List<TablatureTuning>();

        static TablatureTuning()
        {
            Standard = new TablatureTuning("Standard");
            HalfStepDown = new TablatureTuning("Half-Step Down");
            Btuning = new TablatureTuning("B tuning");
            Ctuning = new TablatureTuning("C tuning");
            Dtuning = new TablatureTuning("D tuning");
            DropA = new TablatureTuning("Drop A");
            DropASharp = new TablatureTuning("Drop A#");
            DropB = new TablatureTuning("Drop B");
            DropC = new TablatureTuning("Drop C");
            DropCSharp = new TablatureTuning("Drop C#");
            DropD = new TablatureTuning("Drop D");
            OpenC = new TablatureTuning("Open C");
            OpenD = new TablatureTuning("Open D");
            OpenE = new TablatureTuning("Open E");
            OpenG = new TablatureTuning("Open G");

            NativeTunings = new List<TablatureTuning> {Standard};
            KnownTunings.AddRange(NativeTunings);
        }

        public static TablatureTuning[] GetKnownTunings()
        {
            return KnownTunings.ToArray();
        }

        public static TablatureTuning[] GetNativeTunings()
        {
            return NativeTunings.ToArray();
        }

        public static void RegisterTuning(TablatureTuning tuning)
        {
            if (!IsRegistered(tuning))
                KnownTunings.Add(tuning);
        }

        public static void UnregisterTuning(TablatureTuning tuning)
        {
            KnownTunings.Remove(tuning);
        }

        public static bool IsRegistered(TablatureTuning tuning)
        {
            return KnownTunings.Contains(tuning);
        }

        public static TablatureTuning GetTuningByName(string name, bool createIfMissing = false)
        {
            var match = KnownTunings.Find(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (match != null)
            {
                return match;
            }

            if (createIfMissing)
            {
                var tuning = new TablatureTuning(name);
                RegisterTuning(tuning);
                return tuning;
            }

            return null;
        }

        #endregion
    }
}