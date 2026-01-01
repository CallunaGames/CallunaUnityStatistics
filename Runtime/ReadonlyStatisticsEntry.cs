namespace Calluna.Statistics
{
    public interface ReadonlyStatisticsEntry<T>
    {
        public ReadonlyObservable<T> ReadonlyValue { get; }
    }
}