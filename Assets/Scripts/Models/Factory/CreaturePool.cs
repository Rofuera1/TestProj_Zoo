using UnityEngine;

namespace Core
{
    public class CreaturePool : ObjectPool<Creature>
    {
        private void Awake()
        {
            Init(15);
        }
    }
}