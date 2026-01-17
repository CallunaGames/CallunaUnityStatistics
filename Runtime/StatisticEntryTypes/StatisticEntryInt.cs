using System;
using System.Globalization;
using UnityEngine;

namespace Calluna.Statistics
{
    [CreateAssetMenu(fileName = "Int", menuName = "Calluna Games/Statistics/Types/Int")]
    public class StatisticEntryInt : StatisticEntryType<int>
    {
        protected override bool TryConvertTo(IConvertible convertible, out int result, CultureInfo cultureInfo)
        {
            result = convertible.ToInt32(cultureInfo);
            return true;
        }
        
        protected override bool CompareTo(int value, int other, out int compareValue)
        {
            compareValue = value.CompareTo(other);
            return true;
        }

        protected override bool ChangeBy(int value, int other, out int result)
        {
            result = value + other;
            return true;
        }
    }
}