using Calluna.DI;
using UnityEngine;

namespace Calluna.Statistics.Samples.Basics
{
    public class StatisticsBasicsInstaller : MonoInstaller
    {
        public override void InstallBindings(Binder binder)
        {
            binder.BindToNewSelf<Statistics>().AsSingle();
        }
    }
}
