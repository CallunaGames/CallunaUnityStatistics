using System;
using System.Collections.Generic;
using Calluna.DI;

namespace Calluna.Statistics
{
    public class Statistics : Injectable
    {
        private readonly Dictionary<StatisticId, StatisticsEntry> _statistics =
            new Dictionary<StatisticId, StatisticsEntry>();
        private StatisticEntryFactory _entryFactory;

        public void Inject(Resolver resolver)
        {
            _entryFactory = resolver.Resolve<StatisticEntryFactory>();
        }
        
        public StatisticsEntry GetOrCreateEntry(StatisticId statisticId)
        {
            if (!_statistics.TryGetValue(statisticId, out StatisticsEntry entry))
            {
                entry = _entryFactory.Create(statisticId);
                _statistics.Add(statisticId, entry);
            }

            return entry;
        }

        public StatisticsEntry<T> GetOrCreateEntry<T>(StatisticId statisticId)
        {
            StatisticsEntry entry = GetOrCreateEntry(statisticId);

            if (entry is not StatisticsEntry<T> concreteEntry)
            {
                throw new InvalidOperationException(
                    $"Cannot get entry for statistic {statisticId} due to a value type mismatch: {typeof(T)} | {entry.GetType()}");
            }

            return concreteEntry;
        }

        public IEnumerable<StatisticsEntry> GetAllEntries()
        {
            foreach (StatisticsEntry entry in _statistics.Values)
            {
                yield return entry;
            }
        }
    }
}