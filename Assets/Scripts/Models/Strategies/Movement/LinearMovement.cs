using UnityEngine;

namespace Core
{
    public class LinearMovement : IMovementStrategy
    {
        private Rigidbody Rigidbody;
        private float Velocity;

        public LinearMovement(Rigidbody RB, float Velocity)
        {
            Rigidbody = RB;
            this.Velocity = Velocity;
        }

        public void Move(float DeltaTime)
        {
            Vector3 movement = Rigidbody.transform.forward * Velocity * DeltaTime;
            Vector3 newPosition = Rigidbody.position + movement;

            Rigidbody.MovePosition(newPosition);
        }

        public void Start()
        {
        }
    }
}