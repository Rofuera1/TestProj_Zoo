using UnityEngine;

namespace Core
{
    public class JumpingMovement : IMovementStrategy
    {
        private Rigidbody RB;
        private float JumpForce;
        private float JumpDelay;

        private float TimeAfterPreviousJump;

        public JumpingMovement(Rigidbody rB, float jumpForce, float jumpDelay)
        {
            RB = rB;
            JumpForce = jumpForce;
            JumpDelay = jumpDelay;
        }

        public void Start()
        {
            RB.linearVelocity = RB.transform.forward;
        }

        public void MoveOnUpdate(float DeltaTime)
        {
            if (Mathf.Approximately(RB.linearVelocity.y, 0f)) // Sometimes, they jump off each other, but it's fr more like a фича then a bug
            {
                TimeAfterPreviousJump += DeltaTime;

                if(TimeAfterPreviousJump > JumpDelay)
                    Jump();
            }
        }

        private void Jump()
        {
            RB.AddForce(VectorAt45DegreesUp() * JumpForce, ForceMode.Impulse);
            TimeAfterPreviousJump = 0f;
        }

        private Vector3 VectorAt45DegreesUp()
        {
            Vector3 NewDirection = (RB.transform.forward + Vector3.up).normalized;

            return NewDirection.normalized;
        }
    }
}