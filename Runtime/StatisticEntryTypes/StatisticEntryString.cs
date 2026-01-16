using System;
using System.Globalization;
using UnityEngine;

namespace Calluna.Statistics
{
    [CreateAssetMenu(fileName = "String", menuName = "Calluna Games/Statistics/Types/String")]
    public class StatisticEntryString : StatisticEntryType<string>
    {
        protected override bool TryConvertTo(IConvertible convertible, out string result, CultureInfo cultureInfo)
        {
            result = convertible.ToString(cultureInfo);
            return true;
        }
    }
}