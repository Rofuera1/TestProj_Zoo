using UnityEngine;
using System.Collections.Generic;

namespace Core
{
    public class StatisticsManager : MonoBehaviour
    {
        [Zenject.Inject] Zenject.SignalBus Signaller;

        private Statistics Stats;

        private void Awake()
        {
            Stats = new Statistics();
            Signaller.Fire(Stats); // For refreshing ui and stuff

            Signaller.Subscribe<Creature.CreatureAction>(OnCreatureAction);
        }

        private void OnCreatureAction(Creature.CreatureAction Action)
        {
            switch(Action.Action)
            {
                case Creature.EventTypes.OnDied:
                    OnCreatureDied(Action.Creature);
                    break;
            }

            Signaller.Fire(Stats);
        }

        private void OnCreatureDied(Creature Creature)
        {
            Stats.AddKilledCreature(Creature.CreatureType);
        }
    }

    public class Statistics
    {
        private Dictionary<FoodChainTypes, int> KilledCreatures = new Dictionary<FoodChainTypes, int>();

        public void AddKilledCreature(FoodChainTypes Type)
        {
            if (!KilledCreatures.ContainsKey(Type)) KilledCreatures.Add(Type, 0);

            KilledCreatures[Type]++;
        }

        public int GetKilledCreature(FoodChainTypes Type)
        {
            if (!KilledCreatures.ContainsKey(Type)) return 0;
            return KilledCreatures[Type];
        }
    }
}