using System;
using UnityEngine;

namespace Calluna.Statistics
{
    public abstract class StatisticEntryType : ScriptableObject
    {
        public abstract Type Type { get; }
    }

    public abstract class StatisticEntryType<TValue> : StatisticEntryType
    {
        public override Type Type => typeof(TValue);
    }
}
