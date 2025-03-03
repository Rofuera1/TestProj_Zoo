using UnityEngine;
using Zenject;

namespace Core
{
    public enum CreatureTypes // Самый простой для понимания способ где-то хранить всех возможных животных при малых объемах
    {
        Frog,
        Snake
    }

    public class CreaturesFactory : MonoBehaviour
    {
        [Inject] private CreaturePool Pool;
        [Inject] private CreaturesPainter Painter;

        [Inject] private DiContainer Container;
        [Inject] private SignalBus Signaller;

        private void Awake()
        {
            Signaller.Subscribe<Creature.CreatureDiedSignal>(CreatureDied);
        }

        public void CreateCreature(CreatureTypes CreatureType)
        {
            IMovementStrategy Movement = null;
            IFightingStrategy Fighting = null;
            FoodChainTypes Type = 0;
            Creature NewCreature = Pool.CreateCreature();

            switch(CreatureType)
            {
                case CreatureTypes.Frog:
                    Movement = new JumpingMovement(NewCreature.GetComponent<Rigidbody>(), 5f, 1f);
                    Fighting = Container.Instantiate<DefendingStrategy>();
                    Type = FoodChainTypes.Prey;
                    break;
                case CreatureTypes.Snake:
                    Movement = new LinearMovement(NewCreature.GetComponent<Rigidbody>(), 5f);
                    Fighting = Container.Instantiate<AttackingStrategy>();
                    Type = FoodChainTypes.Predator;
                    break;
            }

            NewCreature.Init(Type, Movement, Fighting);
            Painter.PaintCreatureOnStart(NewCreature);
        }

        public void CreatureDied(Creature.CreatureDiedSignal CreatureSignal)
        {
            Pool.ReturnCreatureBackToPool(CreatureSignal.Creature);
        }
    }
}