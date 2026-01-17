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

        protected override bool CompareTo(bool value, bool other, out int compareValue)
        {
            compareValue = value.CompareTo(other);
            return true;
        }

        protected override bool ChangeBy(bool value, bool other, out bool result)
        {
            result = default;
            return false;
        }
    }
}