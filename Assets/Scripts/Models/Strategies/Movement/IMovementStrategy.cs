using UnityEngine;

namespace Core
{
    public interface IMovementStrategy
    {
        public void Start();
        public void Move(float DeltaTime);
    }
}