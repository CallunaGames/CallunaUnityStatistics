using System;
using System.Globalization;
using UnityEngine;

namespace Calluna.Statistics
{
    [CreateAssetMenu(fileName = "Long", menuName = "Calluna Games/Statistics/Types/Long")]
    public class StatisticEntryLong : StatisticEntryType<long>
    {
        protected override bool TryConvertTo(IConvertible convertible, out long result, CultureInfo cultureInfo)
        {
            result = convertible.ToInt32(cultureInfo);
            return true;
        }
        
        protected override bool CompareTo(long value, long other, out int compareValue)
        {
            compareValue = value.CompareTo(other);
            return true;
        }

        protected override bool ChangeBy(long value, long other, out long result)
        {
            result = value + other;
            return true;
        }
    }
}