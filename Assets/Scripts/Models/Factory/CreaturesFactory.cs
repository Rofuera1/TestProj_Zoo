using UnityEngine;

namespace Core
{
    public enum CreatureTypes // Самый простой для понимания способ где-то хранить всех возможных животных при малых объемах
    {
        Frog,
        Snake
    }

    public class CreaturesFactory : MonoBehaviour // Возможно, было бы проще с zenject, но мне нравится реализовывать свои фабрики + легко использовать пул
    {
        [Zenject.Inject] private CreaturePool Pool;

        public void CreateCreature(CreatureTypes CreatureType) // TODO: Добавить обработчик ошибок
        {
            IMovementStrategy Movement = null;
            IFightingStrategy Fighting = null;
            FoodChainTypes Type = 0;
            Creature NewCreature = Pool.CreateCreature();

            switch(CreatureType)
            {
                case CreatureTypes.Frog:
                    break;
                case CreatureTypes.Snake:
                    Movement = new LinearMovement(NewCreature.GetComponent<Rigidbody>(), 5f);
                    Fighting = new AttackingStrategy();
                    Type = FoodChainTypes.Predator;
                    break;
            }

            NewCreature.Init(Type, Movement, Fighting);
        }
    }
}