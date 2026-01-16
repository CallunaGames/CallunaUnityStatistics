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
    }
}