using UnityEngine;

namespace Core
{
    public interface IMovementStrategy
    {
        public void Move(float DeltaTime);
    }
}