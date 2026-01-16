using Calluna.DI;
using UnityEngine;
using UnityEngine.UI;

namespace Calluna.Statistics.Samples.Basics
{
    public class ButtonPressedStatisticsUpdater : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] private Button _button;
        [SerializeField] private StatisticId _id;

        private Statistics _statistics;
        private StatisticsEntry _statisticsEntry;

        public void Inject(Resolver resolver)
        {
            _statistics = resolver.Resolve<Statistics>();
        }

        public void Initialize()
        {
            _statisticsEntry = _statistics.GetOrCreateEntry(_id);
            _button.onClick.AddListener(IncrementPressedStatistics);
        }

        public void Clean()
        {
            _button.onClick.RemoveListener(IncrementPressedStatistics);
        }

        private void IncrementPressedStatistics()
        {
            _statisticsEntry.SetValue(_statisticsEntry.GetValue<int>() + 1);
        }
    }
}
