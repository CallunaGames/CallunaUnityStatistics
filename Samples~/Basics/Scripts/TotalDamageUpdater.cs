using Calluna.DI;
using UnityEngine;
using UnityEngine.UI;

namespace Calluna.Statistics.Samples.Basics
{
    public class TotalDamageUpdater : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] private Button _button;
        [SerializeField] private StatisticId _id;
        [SerializeField] private Vector2 _damageRange = new Vector2(10, 25);

        private Statistics _statistics;
        private StatisticsEntry<float> _statisticsEntry;
        private System.Random _random = new System.Random();

        public void Inject(Resolver resolver)
        {
            _statistics = resolver.Resolve<Statistics>();
        }

        public void Initialize()
        {
            _statisticsEntry = _statistics.GetOrCreateEntry<float>(_id);
            _button.onClick.AddListener(AddDamage);
        }

        public void Clean()
        {
            _button.onClick.RemoveListener(AddDamage);
        }

        private void AddDamage()
        {
            _statisticsEntry.Value.Value += 
                (float)_random.NextDouble() * (_damageRange.y - _damageRange.x) + _damageRange.x;
        }
    }
}
