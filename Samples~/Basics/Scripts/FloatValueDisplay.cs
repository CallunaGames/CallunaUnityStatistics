using Calluna.DI;
using TMPro;
using UnityEngine;

namespace Calluna.Statistics.Samples.Basics
{
    public class FloatValueDisplay : MonoBehaviour, Injectable, Initializable, Cleanable
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private string _format;

        private ReadonlyObservable<float> _value;
        
        public void Inject(Resolver resolver)
        {
            _value = resolver.Resolve<ReadonlyObservable<float>>();
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
            _text.text = _value.Value.ToString(_format);
        }
    }
}
