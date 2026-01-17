using System.Globalization;
using Calluna.DI;

namespace Calluna.Statistics.Samples.Basics
{
    public class StatisticsBasicsInstaller : MonoInstaller
    {
        public override void InstallBindings(Binder binder)
        {
            binder.BindToNewSelf<Statistics>().AsSingle();
            binder.BindInstance(CultureInfo.CurrentCulture);
            binder.BindToNewSelf<StatisticEntryFactory>().AsSingle();
        }
    }
}
