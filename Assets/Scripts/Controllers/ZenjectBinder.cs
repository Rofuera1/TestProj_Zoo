using Zenject;

namespace Core
{
    public class ZenjectBinder : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            DeclareSignals();

            Container.Bind<CreaturePool>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CreaturesFactory>().FromComponentInHierarchy().AsSingle();
            Container.Bind<FightReferee>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CreaturesPainter>().FromComponentInHierarchy().AsSingle();

            BindStates();
        }

        private void BindStates()
        {
            Container.BindFactory<PathChooser, StateChoosingNewPath, StateChoosingNewPath.Factory>().FromNew();
            Container.BindFactory<StateMoving, StateMoving.Factory>().FromNew();
            Container.BindFactory<Creature, StateFighting, StateFighting.Factory>().FromNew();
            Container.BindFactory<StateDying, StateDying.Factory>().FromNew();
            Container.BindFactory<Creature, StateKilling, StateKilling.Factory>().FromNew();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<Creature.CreatureAction>();
            Container.DeclareSignal<Statistics>();
        }
    }
}