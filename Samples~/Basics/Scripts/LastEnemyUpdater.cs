using System.Collections.Generic;
using Calluna.DI;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Calluna.Statistics.Samples.Basics
{
    public class LastEnemyUpdater : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] private Button _button;
        [SerializeField] private StatisticId _id;

        private readonly List<string> _enemies = new List<string>()
        {
            "Bloodlusty Hobgoblin",
            "Dragonfly",
            "Franz the destroyer",
            "Bandit Thug Lord",
            "Giant Rat"
        };
        
        private Statistics _statistics;
        private StatisticsEntry<string> _statisticsEntry;
        private System.Random _random;

        public void Inject(Resolver resolver)
        {
            _statistics = resolver.Resolve<Statistics>();
            _random = new Random();
        }

        public void Initialize()
        {
            _statisticsEntry = _statistics.GetOrCreateEntry<string>(_id);
            _button.onClick.AddListener(UpdateEnemy);
        }

        public void Clean()
        {
            _button.onClick.RemoveListener(UpdateEnemy);
        }

        private void UpdateEnemy()
        {
            _statisticsEntry.Value.Value = PickRandomEnemy();
        }

        private string PickRandomEnemy()
        {
            return _enemies[_random.Next(_enemies.Count)];
        }
    }
}
