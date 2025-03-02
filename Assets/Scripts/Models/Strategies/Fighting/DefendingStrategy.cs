using UnityEngine;

namespace Core
{
    public class DefendingStrategy : IFightingStrategy
    {
        private Creature Creature;
        private Creature Enemy;
        private FightSession Fight;

        public void OnFight(Creature Creature, Creature Enemy, FightSession Fight)
        {
            this.Creature = Creature;
            this.Enemy = Enemy;
            this.Fight = Fight;
            
            Fight.OnAlive += OnAlive;
            Fight.OnDead += OnDied;

            Fight.AddFleer(Creature);
        }

        private void OnAlive(Creature Creature)
        {
            if(this.Creature == Creature)
            {
                Creature.ChangeState(new StateChoosingNewPath(PathChooser.Random));
                UnsubscribeFromFight();
            }
        }

        private void OnDied(Creature Creature)
        {
            if(this.Creature == Creature)
            {
                Creature.ChangeState(new StateDying());
                UnsubscribeFromFight();
            }
        }

        private void UnsubscribeFromFight()
        {
            Fight.OnAlive -= OnAlive;
            Fight.OnDead -= OnDied;
        }
    }
}