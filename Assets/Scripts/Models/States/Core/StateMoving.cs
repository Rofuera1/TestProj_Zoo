using UnityEngine;

namespace Core
{
    public class StateMoving : State
    {
        private Creature Creature;
        private IMovementStrategy Movement;

        [Zenject.Inject] private StateFighting.Factory FightingFactory;
        [Zenject.Inject] private StateChoosingNewPath.Factory PathFactory;

        public class Factory : Zenject.PlaceholderFactory<StateMoving>
        {
        }

        public override void OnStartState(Creature Creature)
        {
            this.Creature = Creature;
            Movement = Creature.Movement;

            Movement.Start();

            SubscribeToEvents();
        }

        public override void OnUpdate()
        {
            Movement.MoveOnUpdate(Time.deltaTime);
        }

        private void OnCollided(Collision Collision)
        {
            Creature enemy = Collision.collider.GetComponent<Creature>();

            if (enemy)
                Creature.ChangeState(FightingFactory.Create(enemy));
            else if (Collision.transform.tag == "Border")
                Creature.ChangeState(PathFactory.Create(PathChooser.ToCenter));
            else if (Collision.transform.tag == "Wall")
                Creature.ChangeState(PathFactory.Create(PathChooser.Random));
        }

        private void OnTriggerCollided(Collider Collider)
        {
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