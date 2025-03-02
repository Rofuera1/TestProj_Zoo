using UnityEngine;

public class ObjectPool<T>: MonoBehaviour where T: MonoBehaviour
{
    [SerializeField] private T[] ObjectsOnScene;
    [SerializeField] private T Prefab;
    [SerializeField] private Transform PositionToSpawnAt;

    private int CurrentFlag = 0;

    public void Init(int StartAmount)
    {
        ObjectsOnScene = new T[StartAmount];
        for (int i = 0; i < StartAmount; i++)
        {
            ObjectsOnScene[i] = Instantiate(Prefab, PositionToSpawnAt.position, Quaternion.identity);
            ObjectsOnScene[i].gameObject.SetActive(false);
        }
    }

    public T CreateCreature()
    {
        if (CurrentFlag == ObjectsOnScene.Length) return CreateOffPool();

        T CreatureToReturn = ObjectsOnScene[CurrentFlag];
        CreatureToReturn.gameObject.SetActive(true);
        CreatureToReturn.transform.position = PositionToSpawnAt.position;

        CurrentFlag++;

        return CreatureToReturn;
    }

    private void ReturnCreatureBackToPool(T Creature)
    {
        if (CurrentFlag == 0) Destroy(Creature);
        else
        {
            CurrentFlag--;
            ObjectsOnScene[CurrentFlag] = Creature;

            Creature.gameObject.SetActive(false);
        }
    }

    private T CreateOffPool()
    {
        return Instantiate(Prefab, PositionToSpawnAt.position, Quaternion.identity);
    }
}