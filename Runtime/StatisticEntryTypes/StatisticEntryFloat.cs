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
        
        protected override bool CompareTo(float value, float other, out int compareValue)
        {
            compareValue = value.CompareTo(other);
            return true;
        }

        protected override bool ChangeBy(float value, float other, out float result)
        {
            result = value + other;
            return true;
        }
    }
}