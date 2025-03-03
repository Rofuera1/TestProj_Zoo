using UnityEngine;

namespace Core
{
    public interface IMovementStrategy
    {
        public void Start();
        public void MoveOnUpdate(float DeltaTime);
    }
}