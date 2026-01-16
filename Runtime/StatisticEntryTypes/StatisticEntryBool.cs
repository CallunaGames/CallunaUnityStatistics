using System;
using System.Globalization;
using UnityEngine;

namespace Calluna.Statistics
{
    [CreateAssetMenu(fileName = "Bool", menuName = "Calluna Games/Statistics/Types/Bool")]
    public class StatisticEntryBool : StatisticEntryType<bool>
    {
        protected override bool TryConvertTo(IConvertible convertible, out bool result, CultureInfo cultureInfo)
        {
            result = convertible.ToBoolean(cultureInfo);
            return true;
        }
    }
}