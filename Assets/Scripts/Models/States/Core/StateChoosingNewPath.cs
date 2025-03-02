using UnityEngine;

namespace Core
{
    public class StateChoosingNewPath : State
    {
        private Creature Machine;

        public override void OnStartState(Creature Machine)
        {
            this.Machine = Machine;
        }
        public override void OnUpdate()
        {
        }

        public override void OnEndState()
        {
            Machine.ChangeState(new StateMoving());
        }
    }
}