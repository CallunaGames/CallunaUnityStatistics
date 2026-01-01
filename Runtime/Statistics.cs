using System;
using System.Collections.Generic;

namespace Calluna.Statistics
{
    public class Statistics
    {
        private readonly Dictionary<ScriptableObjectId, StatisticsEntry> _statistics =
            new Dictionary<ScriptableObjectId, StatisticsEntry>();
        
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
                throw new InvalidOperationException($"Cannot get entry for statistic {statisticId} due to a value type mismatch: {typeof(T)} | {entry.GetType()}");
            }
            
            return concreteEntry;
        }
    }
}
