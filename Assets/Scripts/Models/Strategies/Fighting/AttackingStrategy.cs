namespace Core
{
    public class AttackingStrategy : IFightingStrategy
    {
        private Creature Creature;
        private Creature Enemy;
        private FightSession CurrentFight;
        public int AttackDamage { get; private set; }

        public void OnFight(Creature Creature, Creature Enemy, FightSession Fight)
        {
            CurrentFight = Fight;

            CurrentFight.OnAliveAndKilled += OnAliveAndKilled;
            CurrentFight.OnDead += OnDied;

            CurrentFight.AddKiller(Creature);
        }

        private void OnDied(Creature creature)
        {
            if (Creature == creature)
            {
                Creature.ChangeState(new StateDying());
                UnsubscribeFromFight();
            }
        }

        private void OnAliveAndKilled(Creature creature)
        {
            if (Creature == creature)
            {
                Creature.ChangeState(new StateKilling(Enemy));
                UnsubscribeFromFight();
            }
        }

        private void UnsubscribeFromFight()
        {
            CurrentFight.OnAliveAndKilled -= OnAliveAndKilled;
            CurrentFight.OnDead -= OnDied;
        }
    }
}