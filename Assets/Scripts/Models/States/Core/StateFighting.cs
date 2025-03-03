using UnityEngine;

namespace Core
{
    public class StateFighting : State
    {
        private Creature Creature;
        private Creature Enemy;

        [Zenject.Inject] private FightReferee Referee;
        private IFightingStrategy FightingStrategy;

        public class Factory: Zenject.PlaceholderFactory<Creature, StateFighting>
        {

        }

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
            FightSession Session = Referee.GetCurrentFight(Creature, Enemy);
            FightingStrategy.OnFight(Creature, Enemy, Session);
        }
    }
}