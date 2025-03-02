using UnityEngine;

namespace Core
{
    public class StateKilling : State
    {
        private Creature KillingWho;
        private Creature Creature;

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
            // some animation with the use of async?

            // TODO: call counter

            Creature.ChangeState(new StateMoving());
        }

        public override void OnUpdate()
        {
        }

        public override void OnEndState()
        {
        }
    }
}