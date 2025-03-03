using UnityEngine;
using Zenject;

namespace Core
{
    public class CreaturePool : MonoBehaviour
    {
        [SerializeField] private Creature[] ObjectsOnScene;
        [SerializeField] private Creature Prefab;
        [SerializeField] private Transform PositionToSpawnAt;
        [SerializeField] private Transform ParentToSpawnUnder;

        [Inject] private DiContainer Container;

        private int CurrentFlag = 0;

        private void Awake()
        {
            Init(10);
        }

        public void Init(int StartAmount)
        {
            ObjectsOnScene = new Creature[StartAmount];
            for (int i = 0; i < StartAmount; i++)
            {
                ObjectsOnScene[i] = CreateOffPool();
                ObjectsOnScene[i].gameObject.SetActive(false);
            }
        }

        public Creature CreateCreature()
        {
            if (CurrentFlag == ObjectsOnScene.Length) return CreateOffPool();

            Creature CreatureToReturn = ObjectsOnScene[CurrentFlag];
            CreatureToReturn.gameObject.SetActive(true);
            CreatureToReturn.transform.position = PositionToSpawnAt.position;

            CurrentFlag++;

            return CreatureToReturn;
        }

        public void ReturnCreatureBackToPool(Creature Creature)
        {
            if (CurrentFlag == 0) Destroy(Creature.gameObject);
            else
            {
                CurrentFlag--;
                ObjectsOnScene[CurrentFlag] = Creature;

                Creature.gameObject.SetActive(false);
            }
        }

        private Creature CreateOffPool()
        {
            return Container.InstantiatePrefabForComponent<Creature>(
                            Prefab,
                            PositionToSpawnAt.position,
                            Quaternion.identity,
                            ParentToSpawnUnder);
        }
    }
}