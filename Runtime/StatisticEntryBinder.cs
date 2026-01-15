using Calluna.DI;

namespace Calluna.Statistics
{
    public abstract class StatisticEntryBinder
    {
        public abstract void Bind(Binder binder, Statistics statistics, StatisticId id);
    }
    
    public class StatisticEntryBinder<T> : StatisticEntryBinder
    {
        public override void Bind(Binder binder, Statistics statistics, StatisticId id)
        {
            StatisticsEntry<T> entry = statistics.GetOrCreateEntry<T>(id);
            binder.Bind<ReadonlyObservable<T>>().And<Observable<T>>().ToInstance(entry.Value);
            binder.Bind<ReadonlyStatisticsEntry<T>>().And<StatisticsEntry<T>>().And<StatisticsEntry>()
                .ToInstance(entry);
        }
    }
}