using UnityEngine;

namespace Core
{
    public class StateKilling : State
    {
        private Creature KillingWho;
        private Creature Creature;

        //[Zenject.Inject] private StateMoving.Factory MovingFactory;
        [Zenject.Inject] private StateChoosingNewPath.Factory PathChoosingFactory;

        public class Factory : Zenject.PlaceholderFactory<Creature, StateKilling>
        {
        }

        public StateKilling(Creature Victim)
        {
            KillingWho = Victim;
        }

        public override void OnStartState(Creature Creature)
        {
            this.Creature = Creature;

            OnKillVictim();
        }

        private void OnKillVictim()
        {
            Creature.CallEvent(Creature.EventTypes.OnKilled);
            Creature.ChangeState(PathChoosingFactory.Create(PathChooser.Random));
        }

        public override void OnUpdate()
        {
        }

        public override void OnEndState()
        {
        }
    }
}