using UnityEngine;

namespace Core
{
    public enum CreatureTypes // ����� ������� ��� ��������� ������ ���-�� ������� ���� ��������� �������� ��� ����� �������
    {
        Frog,
        Snake
    }

    public class CreaturesFactory : MonoBehaviour
    {
        [SerializeField] private GameObject CreaturePrefab;

        private CreaturePool Pool;

        public void CreateCreature(CreatureTypes CreatureType) // TODO: �������� ���������� ������
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