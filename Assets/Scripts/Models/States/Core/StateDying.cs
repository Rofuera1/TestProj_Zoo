using UnityEngine;

namespace Core
{
    public class StateDying : State
    {
        private Creature Creature;

        public override void OnStartState(Creature Creature)
        {
            this.Creature = Creature;

            Creature.Die();
        }

        public override void OnUpdate()
        {
        }

        public override void OnEndState()
        {
        }
    }
}