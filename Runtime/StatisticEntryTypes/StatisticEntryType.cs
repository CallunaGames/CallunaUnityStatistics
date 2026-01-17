using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Calluna.Statistics
{
    public abstract class StatisticEntryType : ScriptableObject
    {
        public abstract Type Type { get; }
    }

    public abstract class StatisticEntryType<TValue> : StatisticEntryType
    {
        public override Type Type => typeof(TValue);

        public bool TryConvertTo<TTo>(TValue value, out TTo result, CultureInfo cultureInfo = default)
        {
            result = default;

            if (typeof(TValue) == typeof(TTo))
            {
                result = Unsafe.As<TValue, TTo>(ref value);
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

        public bool ChangeBy<TOther>(TValue value, TOther other, out TValue result, CultureInfo cultureInfo = default)
        {
            double Add(double value1, double value2) => value1 + value2;
            
            result = default;
            if (TryConvertTo(other, out TValue otherValue, cultureInfo) && 
                ChangeBy(value, otherValue, out result))
                return true;
            return BackupOperation(value, otherValue, out result, Add, cultureInfo);
        }

        public bool CompareTo<TOther>(TValue value, TOther other, out int compareValue, CultureInfo cultureInfo = default)
        {
            compareValue = default;
            if (!TryConvertTo(other, out TValue otherValue, cultureInfo))
                return false;
            return CompareTo(value, otherValue, out compareValue);
        }

        protected virtual bool ChangeBy(TValue value, TValue other, out TValue result)
        {
            result = default;
            return false;
        }

        protected virtual bool CompareTo(TValue value, TValue other, out int compareValue)
        {
            compareValue = 0;
            if (value is IComparable<TValue> comparable)
            {
                compareValue = comparable.CompareTo(other);
                return true;
            }

            return false;
        }

        protected virtual bool TryConvertTo(IConvertible convertible, out TValue result, CultureInfo cultureInfo)
        {
            result = (TValue)convertible.ToType(Type, cultureInfo);
            return false;
        }

        private bool BackupOperation(TValue first, TValue second, out TValue result, Func<double, double, double> operation, CultureInfo cultureInfo)
        {
            result = default;
            if (first is not IConvertible firstConvertable || second is not IConvertible secondConvertable)
                return false;
            double firstDouble = firstConvertable.ToDouble(cultureInfo);
            double secondDouble = secondConvertable.ToDouble(cultureInfo);
            double resultDouble = operation(firstDouble, secondDouble);
            result = (TValue)Convert.ChangeType(resultDouble, typeof(TValue));
            return true;
        }
    }
}