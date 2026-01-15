using System;
using Calluna.DI;
using UnityEngine;

namespace Calluna.Statistics
{
    public class StatisticsEntryInstaller : MonoInstaller, Injectable
    {
        [SerializeField] private StatisticId _id;
        private Statistics _statistics;

        public void Inject(Resolver resolver)
        {
            _statistics = resolver.Resolve<Statistics>();
        }
        
        public override void InstallBindings(Binder binder)
        {
            Type binderType = typeof(StatisticEntryBinder<>).MakeGenericType(_id.Type.Type);
            StatisticEntryBinder statisticEntryBinder = (StatisticEntryBinder) Activator.CreateInstance(binderType);
            statisticEntryBinder.Bind(binder, _statistics, _id);
        }
    }
}
