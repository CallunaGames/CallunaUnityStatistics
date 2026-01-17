using System;
using System.Globalization;
using UnityEngine;

namespace Calluna.Statistics
{
    [CreateAssetMenu(fileName = "Double", menuName = "Calluna Games/Statistics/Types/Double")]
    public class StatisticEntryDouble : StatisticEntryType<double>
    {
        protected override bool TryConvertTo(IConvertible convertible, out double result, CultureInfo cultureInfo)
        {
            result = convertible.ToDouble(cultureInfo);
            return true;
        }

        protected override bool CompareTo(double value, double other, out int compareValue)
        {
            compareValue = value.CompareTo(other);
            return true;
        }

        protected override bool ChangeBy(double value, double other, out double result)
        {
            result = value + other;
            return true;
        }
    }
}