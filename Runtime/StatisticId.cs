using System;
using UnityEngine;

namespace Calluna.Statistics
{
    [CreateAssetMenu(fileName = "StatisticId", menuName = "Calluna Games/Statistics/Id")]
    public class StatisticId : ScriptableObjectId, IEquatable<StatisticId>
    {
        [field: SerializeField] public StatisticEntryType Type { get; private set; }
        
        public bool Equals(StatisticId other)
        {
            return ReferenceEquals(other, this);
        }
    }
}
