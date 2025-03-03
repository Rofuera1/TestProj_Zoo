using UnityEngine;

namespace Core
{
    public class JumpingMovement : IMovementStrategy
    {
        private Rigidbody RB;
        private float JumpForce;

        public JumpingMovement(Rigidbody rB, float jumpForce)
        {
            RB = rB;
            JumpForce = jumpForce;
        }

        public void Start()
        {
            RB.linearVelocity = RB.transform.forward;
        }

        public void Move(float DeltaTime)
        {
            if (Mathf.Approximately(RB.linearVelocity.y, 0f)) Jump(); // Sometimes, they jump off each other, but it's fr more like a фича then a bug
        }

        private void Jump()
        {
            RB.AddForce(VectorAt45DegreesUp() * JumpForce, ForceMode.Impulse);
        }

        private Vector3 VectorAt45DegreesUp()
        {
            Vector3 NewDirection = (RB.transform.forward + Vector3.up).normalized;

            return NewDirection.normalized;
        }
    }
}