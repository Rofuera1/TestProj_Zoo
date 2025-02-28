using System;
using UnityEngine;

namespace Core
{
    public enum FoodChainTypes // ��� ������ �� ����� ��� �������� ��������. ��� �������������� ��������� (���� ��������� - �� Prey/Predator), ������������ �������������� ��� Prey-Prey
    {
        Prey,
        Predator
    }

    public class Creature : MonoBehaviour, IStateMachine, ICollidable
    {
        public IFightingStrategy FightingStrategy { get; private set; }
        public IMovementStrategy Movement { get; private set; }
        public Action<Collision> OnCollided { get; set; }
        public Action<Collider> OnTriggerCollided { get ; set; }

        private State CurrentState;

        public void Init(IMovementStrategy Movement, IFightingStrategy FightingStrategy) // ���������� �� �������
        {
            this.Movement = Movement;
            this.FightingStrategy = FightingStrategy;

            ChangeState(new StateChoosingNewPath());
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
    }
}
