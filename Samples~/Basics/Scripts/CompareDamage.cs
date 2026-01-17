using Calluna.DI;
using TMPro;
using UnityEngine;

namespace Calluna.Statistics.Samples.Basics
{
    public class CompareDamage : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] private float _compareValue;
        [SerializeField] private TextMeshProUGUI _text;
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
            _statisticsEntry.OnValueChanged += UpdateText;
            UpdateText();
        }

        public void Clean()
        {
            _statisticsEntry.OnValueChanged -= UpdateText;
        }

        private void UpdateText()
        {
            int compareValue = _statisticsEntry.CompareTo(_compareValue);
            char sign = compareValue > 0 ? '>' : compareValue < 0 ? '<' : '=';
            _text.text = $"Compare Damage: {_statisticsEntry.GetValue<float>():F2} {sign} {_compareValue:F2}";
        }
    }
}
