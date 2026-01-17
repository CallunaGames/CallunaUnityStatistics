using System;
using System.Globalization;
using Calluna.DI;

namespace Calluna.Statistics
{
    public class StatisticEntryFactory : Injectable
    {
        private CultureInfo _cultureInfo;

        public void Inject(Resolver resolver)
        {
            _cultureInfo = resolver.ResolveOptional<CultureInfo>() ?? CultureInfo.InvariantCulture;
        }
        
        public StatisticsEntry Create(StatisticId id)
        {
            Type type = typeof(StatisticsEntry<>).MakeGenericType(id.Type.Type);
            return (StatisticsEntry)Activator.CreateInstance(type, id, _cultureInfo);
        }
    }
}