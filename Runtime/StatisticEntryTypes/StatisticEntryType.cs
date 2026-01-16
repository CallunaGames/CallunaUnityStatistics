using System;
using System.Globalization;
using UnityEngine;

namespace Calluna.Statistics
{
    public abstract class StatisticEntryType : ScriptableObject
    {
        public abstract Type Type { get; }
        public abstract bool TryConvertTo<TFrom, TTo>(TFrom value, out TTo result, CultureInfo cultureInfo = default);
    }

    public abstract class StatisticEntryType<TValue> : StatisticEntryType
    {
        public override Type Type => typeof(TValue);

        public override bool TryConvertTo<TFrom, TTo>(TFrom value, out TTo result, CultureInfo cultureInfo = default)
        {
            result = default;

            if (value is TTo toValue)
            {
                result = toValue;
                return true;
            }
            TValue tValue = default;
            if (typeof(TTo) == typeof(TValue) && TryConvertTo(value, out tValue))
            {
                result = (TTo)(object)tValue;
                return true;
            }
            if (value is IConvertible convertible)
            {
                object obj = convertible.ToType(typeof(TTo), cultureInfo);
                result = (TTo)obj;
                return true;
            }

            return false;
        }

        public bool TryConvertTo<TFrom>(TFrom value, out TValue result, CultureInfo cultureInfo = default)
        {
            result = default;
            if (value is TValue targetValue)
            {
                result = targetValue;
                return true;
            }

            if (value is IConvertible convertible)
            {
                return TryConvertTo(convertible, out result, cultureInfo);
            }

            return false;
        }

        protected virtual bool TryConvertTo(IConvertible convertible, out TValue result, CultureInfo cultureInfo)
        {
            result = default;
            return false;
        }
    }
}
