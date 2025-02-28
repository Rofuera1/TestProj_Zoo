using UnityEngine;

namespace Core
{
    public enum CreatureTypes // Самый простой для понимания способ где-то хранить всех возможных животных при малых объемах
    {
        Frog,
        Snake
    }

    public class CreaturesFactory : MonoBehaviour
    {
        [SerializeField] private GameObject CreaturePrefab;

        private CreaturePool Pool;

        public void CreateCreature(CreatureTypes CreatureType) // TODO: Добавить обработчик ошибок
        {
            IMovementStrategy Movement = null;
            IFightingStrategy Fighting = null;
            Creature NewCreature = Pool.CreateCreature();

            switch(CreatureType)
            {
                case CreatureTypes.Frog:
                    break;
                case CreatureTypes.Snake:
                    Movement = new LinearMovement(NewCreature.GetComponent<Rigidbody>(), 1f);
                    break;
            }

            NewCreature.Init(Movement, Fighting);
        }
    }
}