using UnityEngine;

namespace Core
{
    public enum CreatureTypes // ����� ������� ��� ��������� ������ ���-�� ������� ���� ��������� �������� ��� ����� �������
    {
        Frog,
        Snake
    }

    public class CreaturesFactory : MonoBehaviour // ��������, ���� �� ����� � zenject, �� ��� �������� ������������� ���� ������� + ����� ������������ ���
    {
        [Zenject.Inject] private CreaturePool Pool;

        public void CreateCreature(CreatureTypes CreatureType) // TODO: �������� ���������� ������
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