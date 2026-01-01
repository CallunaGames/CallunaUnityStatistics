using Calluna.DI;
using TMPro;
using UnityEngine;

namespace Calluna.Statistics.Samples.Basics
{
    public class StringValueDisplay : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] private TextMeshProUGUI _text;

        private ReadonlyObservable<string> _value;
        
        public void Inject(Resolver resolver)
        {
            _value = resolver.Resolve<ReadonlyObservable<string>>();
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
            _text.text = _value.Value;
        }
    }
}
