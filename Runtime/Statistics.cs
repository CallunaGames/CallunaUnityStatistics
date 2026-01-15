using System;
using System.Collections.Generic;
using System.Linq;

namespace Calluna.Statistics
{
    public class Statistics
    {
        private readonly Dictionary<StatisticId, StatisticsEntry> _statistics =
            new Dictionary<StatisticId, StatisticsEntry>();
        
        public StatisticsEntry<T> GetOrCreateEntry<T>(StatisticId statisticId)
        {
            if (!_statistics.TryGetValue(statisticId, out StatisticsEntry entry))
            {
                StatisticsEntry<T> result = new StatisticsEntry<T>(statisticId);
                _statistics.Add(statisticId, result);
                return result;
            }

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