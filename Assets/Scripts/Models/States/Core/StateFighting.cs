using UnityEngine;

namespace Core
{
    public class StateFighting : State
    {
        private Creature Creature;
        private Creature Enemy;

        private IFightingStrategy FightingStrategy;

        public StateFighting(Creature Enemy)
        {
            this.Enemy = Enemy;
        }

        public override void OnStartState(Creature Creature)
        {
            this.Creature = Creature;
            this.FightingStrategy = Creature.FightingStrategy;

            ChooseWhoDies();
        }

        public override void OnUpdate()
        {
        }

        public override void OnEndState()
        {
        }

        private void ChooseWhoDies()
        {
            FightSession Session = FightReferee.GetCurrentFight(Creature, Enemy);
            FightingStrategy.OnFight(Creature, Enemy, Session);
        }
    }
}