namespace Calluna.Statistics
{
    public abstract class StatisticsEntry
    {
        public abstract StatisticId Id { get; }
    }

    public class StatisticsEntry<T> : StatisticsEntry, ReadonlyStatisticsEntry<T>
    {
        public Observable<T> Value { get; }
        public ReadonlyObservable<T> ReadonlyValue => Value;
        public override StatisticId Id { get; }

        public StatisticsEntry(StatisticId id)
        {
            Id = id;
            Value = new Observable<T>();
        }

        public StatisticsEntry(StatisticId id, T initalValue)
        {
            Id = id;
            Value = new Observable<T>() { Value = initalValue };
        }
    }
}