using System;
using System.Globalization;
using UnityEngine;

namespace Calluna.Statistics
{
    [CreateAssetMenu(fileName = "Float", menuName = "Calluna Games/Statistics/Types/Float")]
    public class StatisticEntryFloat : StatisticEntryType<float>
    {
        protected override bool TryConvertTo(IConvertible convertible, out float result, CultureInfo cultureInfo)
        {
            result = (float)convertible.ToDouble(cultureInfo);
            return true;
        }
    }
}