using Calluna.DI;
using TMPro;
using UnityEngine;

namespace Calluna.Statistics.Samples.Basics
{
    public class IntValueDisplay : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] private TextMeshProUGUI _text;

        private ReadonlyObservable<int> _value;
        
        public void Inject(Resolver resolver)
        {
            _value = resolver.Resolve<ReadonlyObservable<int>>();
        }

        public void Initialize()
        {
            UpdateText();
            _value.OnChanged += UpdateText;
        }

        public void Clean()
        {
            _value.OnChanged -= UpdateText;
        }

        private void UpdateText()
        {
            _text.text = _value.Value.ToString();
        }
    }
}
