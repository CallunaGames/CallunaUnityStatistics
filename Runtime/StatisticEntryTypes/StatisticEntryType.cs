using System;
using System.Globalization;
using System.Runtime.CompilerServices;
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

            if (typeof(TFrom) == typeof(TTo))
            {
                result = Unsafe.As<TFrom, TTo>(ref value);
                return true;
            }

            if (typeof(TTo) == typeof(TValue) && TryConvertTo(value, out TValue tValue))
            {
                result = Unsafe.As<TValue, TTo>(ref tValue);
                return true;
            }

            if (value is IConvertible convertible)
            {
                result = (TTo)convertible.ToType(typeof(TTo), cultureInfo);
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
            result = (TValue)convertible.ToType(Type, cultureInfo);
            return false;
        }
    }
}