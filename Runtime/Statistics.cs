using System;
using System.Collections.Generic;
using System.Globalization;
using Calluna.DI;

namespace Calluna.Statistics
{
    public class Statistics : Injectable
    {
        private readonly Dictionary<StatisticId, StatisticsEntry> _statistics =
            new Dictionary<StatisticId, StatisticsEntry>();
        private CultureInfo _cultureInfo;

        public void Inject(Resolver resolver)
        {
            _cultureInfo = resolver.ResolveOptional<CultureInfo>() ?? CultureInfo.InvariantCulture;
        }
        
        public StatisticsEntry GetOrCreateEntry(StatisticId statisticId)
        {
            if (!_statistics.TryGetValue(statisticId, out StatisticsEntry entry))
            {
                entry = CreateEntry(statisticId);
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

        private StatisticsEntry CreateEntry(StatisticId statisticId)
        {
            Type type = typeof(StatisticsEntry<>).MakeGenericType(statisticId.Type.Type);
            return (StatisticsEntry)Activator.CreateInstance(type, statisticId, _cultureInfo);
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