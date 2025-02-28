using UnityEngine;

namespace Core
{
    public class StateMoving : State
    {
        private Creature Creature;
        private IMovementStrategy Movement;

        public override void OnStartState(Creature Creature)
        {
            this.Creature = Creature;
            Movement = Creature.Movement;

            SubscribeToEvents();
        }

        public override void OnUpdate()
        {
            Movement.Move(Time.deltaTime);
        }

        private void OnCollided(Collision Collision)
        {
            Creature enemy = Collision.collider.GetComponent<Creature>();

            if (enemy)
                Creature.ChangeState(new StateFighting(enemy));
        }

        private void OnTriggerCollided(Collider Collider)
        {
            if (Collider.transform.tag == "Border")
                Creature.ChangeState(new StateChoosingNewPath());
        }

        public override void OnEndState()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            Creature.OnCollided += OnCollided;
            Creature.OnTriggerCollided += OnTriggerCollided;
        }

        private void UnsubscribeFromEvents()
        {
            Creature.OnCollided -= OnCollided;
            Creature.OnTriggerCollided -= OnTriggerCollided;
        }
    }
}