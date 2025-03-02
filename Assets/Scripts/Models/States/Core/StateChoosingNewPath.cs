using UnityEngine;

namespace Core
{
    public enum PathChooser
    {
        Random,
        ToCenter
    }

    public class StateChoosingNewPath : State
    {
        private Creature Machine;
        private PathChooser Path;

        public StateChoosingNewPath(PathChooser Path)
        {
            this.Path = Path;
        }

        public override void OnStartState(Creature Machine)
        {
            this.Machine = Machine;

            RotateForNewPath();

        }
        public override void OnUpdate()
        {
        }

        public override void OnEndState()
        {
        }

        private void RotateForNewPath()
        {
            switch (Path)
            {
                case PathChooser.ToCenter:
                    RotateTowardsZeroPoint();
                break;
                case PathChooser.Random:
                    RotateRandomly();
                break;
            }
            Machine.ChangeState(new StateMoving());
        }

        private void RotateRandomly()
        {
            Machine.transform.Rotate(Vector3.up * Random.Range(0f, 180f));
        }

        private void RotateTowardsZeroPoint()
        {
            Vector3 CreaturePosition = Machine.transform.position;

            Machine.transform.LookAt(Vector3.up * CreaturePosition.y);
        }
    }
}