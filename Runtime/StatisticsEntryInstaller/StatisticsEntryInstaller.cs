using Calluna.DI;
using UnityEngine;

namespace Calluna.Statistics
{
    public abstract class StatisticsEntryInstaller<T> : MonoInstaller, Injectable
    {
        [SerializeField] private StatisticId _id;
        private Statistics _statistics;

        public void Inject(Resolver resolver)
        {
            _statistics = resolver.Resolve<Statistics>();
        }
        
        public override void InstallBindings(Binder binder)
        {
            StatisticsEntry<T> entry = _statistics.GetOrCreateEntry<T>(_id);
            binder.Bind<ReadonlyObservable<T>>().And<Observable<T>>().ToInstance(entry.Value);
            binder.Bind<ReadonlyStatisticsEntry<T>>().And<StatisticsEntry<T>>().And<StatisticsEntry>()
                .ToInstance(entry);
        }
    }
}
