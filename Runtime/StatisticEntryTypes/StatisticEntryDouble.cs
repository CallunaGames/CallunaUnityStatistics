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
    }
}