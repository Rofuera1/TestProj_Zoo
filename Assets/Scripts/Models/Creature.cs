using System;
using UnityEngine;

namespace Core
{
    public enum FoodChainTypes
    {
        Prey,
        Predator
    }

    public class Creature : MonoBehaviour, IStateMachine, ICollidable, ICaller
    {
        public FoodChainTypes CreatureType { get; private set; }
        public IFightingStrategy FightingStrategy { get; private set; }
        public IMovementStrategy Movement { get; private set; }

        public event Action<Collision> OnCollided;
        public event Action<Collider> OnTriggerCollided;
        public event Action<string> OnAction;

        private State CurrentState;

        public void Init(FoodChainTypes Type, IMovementStrategy Movement, IFightingStrategy FightingStrategy) // Is initialized from fabric
        {
            CreatureType = Type;
            this.Movement = Movement;
            this.FightingStrategy = FightingStrategy;

            ChangeState(new StateChoosingNewPath(PathChooser.Random));
        }

        public void ChangeState(State NewState)
        {
            CurrentState?.OnEndState();
            CurrentState = NewState;
            CurrentState.OnStartState(this);
        }

        private void Update()
        {
            CurrentState?.OnUpdate();
        }

        public void OnCollisionEnter(Collision collision)
        {
            OnCollided?.Invoke(collision);
        }

        public void OnTriggerEnter(Collider other)
        {
            OnTriggerCollided?.Invoke(other);
        }

        public void CallEvent(string Event) // Perhaps, it's better to use enum - but for now strings will work just fine
        {
            OnAction?.Invoke(Event);
        }

        public void Die()
        {
            CallEvent("OnDied");

            // TODO: bind with zenject
        }
    }
}