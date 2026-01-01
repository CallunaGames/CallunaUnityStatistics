using Calluna.DI;
using TMPro;
using UnityEngine;

namespace Calluna.Statistics.Samples.Basics
{
    public class StatisticsDisplay : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private StatisticId _statisticId;

        private Statistics _statistics;
        private StatisticsEntry<int> _entry;

        public void Inject(Resolver resolver)
        {
            _statistics = resolver.Resolve<Statistics>();
        }

        public void Initialize()
        {
            _entry = _statistics.GetOrCreateEntry<int>(_statisticId);
            _entry.Value.OnChanged += UpdateDisplay;
            UpdateDisplay();
        }

        public void Clean()
        {
            _entry.Value.OnChanged -= UpdateDisplay;
        }

        private void UpdateDisplay()
        {
            _text.text = _entry.Value.Value.ToString();
        }
    }
}
