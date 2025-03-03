namespace Core
{
    public class AttackingStrategy : IFightingStrategy
    {
        private Creature Creature;
        private Creature Enemy;
        private FightSession CurrentFight;
        public int AttackDamage { get; private set; }

        [Zenject.Inject] private StateDying.Factory DyingFactory;
        [Zenject.Inject] private StateKilling.Factory KillingFactory;

        public void OnFight(Creature Creature, Creature Enemy, FightSession Fight)
        {
            this.Creature = Creature;
            this.Enemy = Enemy;
            CurrentFight = Fight;

            CurrentFight.OnAliveAndKilled += OnAliveAndKilled;
            CurrentFight.OnDead += OnDied;

            CurrentFight.AddKiller(Creature);
        }

        private void OnDied(Creature creature)
        {
            if (Creature == creature)
            {
                Creature.ChangeState(DyingFactory.Create());
                UnsubscribeFromFight();
            }
        }

        private void OnAliveAndKilled(Creature creature)
        {
            if (Creature == creature)
            {
                Creature.ChangeState(KillingFactory.Create(Enemy));
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