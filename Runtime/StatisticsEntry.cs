using System;
using System.Globalization;

namespace Calluna.Statistics
{
    public abstract class StatisticsEntry
    {
        public abstract event Observable.ValueChanged OnValueChanged;
        public abstract StatisticId Id { get; }
        
        public abstract void SetValue<T>(T value);
        public abstract T GetValue<T>();
        public abstract void ChangeValueBy<T>(T value);
        /// <summary>
        /// Compares the statistic value with value.
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns><![CDATA[1 equals statistic value > value. 0 equals statistic value == value. -1 equals statistic value < value]]></returns>
        public abstract int CompareTo<T>(T value);
    }

    public class StatisticsEntry<TValue> : StatisticsEntry, ReadonlyStatisticsEntry<TValue>
    {
        public Observable<TValue> Value { get; }
        public ReadonlyObservable<TValue> ReadonlyValue => Value;
        public override event Observable.ValueChanged OnValueChanged
        {
            add => Value.OnChanged += value;
            remove => Value.OnChanged -= value;
        }
        public event Observable<TValue>.ValueChangedWithValues OnValueChangedWithValues
        {
            add => Value.OnChangedWithValues += value;
            remove => Value.OnChangedWithValues -= value;
        }
        public override StatisticId Id { get; }

        private CultureInfo _cultureInfo;
        private StatisticEntryType<TValue> _type;

        public StatisticsEntry(StatisticId id, CultureInfo cultureInfo) : this(id, default, cultureInfo)
        {
            
        }

        public StatisticsEntry(StatisticId id, TValue initialValue, CultureInfo cultureInfo)
        {
            Id = id;
            Value = new Observable<TValue>() { Value = initialValue };
            _cultureInfo = cultureInfo;
            _type = (StatisticEntryType<TValue>)id.Type;
        }

        public override void SetValue<T>(T value)
        {
            if (!_type.TryConvertTo(value, out TValue result, _cultureInfo))
                throw new ArgumentException(
                    $"Failed to set value {value} to statistic {Id}. Can not convert {typeof(T)} to {typeof(TValue)}.");
            Value.Value = result;
        }

        public override T GetValue<T>()
        {
            if (!_type.TryConvertTo(Value.Value, out T result, _cultureInfo))
                throw new ArgumentException(
                    $"Failed to get value from statistic {Id}. Can not convert {typeof(T)} to {typeof(TValue)}.");
            return result;
        }

        public override void ChangeValueBy<T>(T value)
        {
            if (!_type.ChangeBy(Value.Value, value, out TValue result, _cultureInfo))
                throw new ArgumentException(
                    $"Failed to change value from statistic {Id} by {value}. Can not convert {typeof(T)} to {typeof(TValue)} or one of the values is not a number");
            Value.Value = result;
        }

        public override int CompareTo<T>(T value)
        {
            if(!_type.CompareTo(Value.Value, value, out int result, _cultureInfo))
                throw new ArgumentException(
                    $"Failed to compare value from statistic {Id} to {value}. Can not convert {typeof(T)} to {typeof(TValue)} or the values are not comparable");
            return result;
        }
    }
}