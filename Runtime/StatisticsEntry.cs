

using System;
using System.Globalization;

namespace Calluna.Statistics
{
    public abstract class StatisticsEntry
    {
        public abstract StatisticId Id { get; }
        public abstract void SetValue<T>(T value);
        public abstract T GetValue<T>();
    }

    public class StatisticsEntry<TValue> : StatisticsEntry, ReadonlyStatisticsEntry<TValue>
    {
        public Observable<TValue> Value { get; }
        public ReadonlyObservable<TValue> ReadonlyValue => Value;
        public override StatisticId Id { get; }

        private CultureInfo _cultureInfo;

        public StatisticsEntry(StatisticId id, CultureInfo cultureInfo = default) : this(id, default, cultureInfo)
        {
            Id = id;
            Value = new Observable<TValue>();
            _cultureInfo = cultureInfo;
        }

        public StatisticsEntry(StatisticId id, TValue initialValue, CultureInfo cultureInfo = default)
        {
            Id = id;
            Value = new Observable<TValue>() { Value = initialValue };
            _cultureInfo = cultureInfo;
        }
        
        public override void SetValue<T>(T value)
        {
            if (!Id.Type.TryConvertTo(value, out TValue result, _cultureInfo))
                throw new ArgumentException($"Failed to set value {value} to statistic {Id}. Can not convert {typeof(T)} to {typeof(TValue)}.");
            Value.Value = result;
        }

        public override T GetValue<T>()
        {
            if (!Id.Type.TryConvertTo(Value.Value, out T result, _cultureInfo))
                throw new ArgumentException($"Failed to get value from statistic {Id}. Can not convert {typeof(T)} to {typeof(TValue)}.");
            return result;
        }
    }
}