using UnityEngine;

namespace Core.Effects
{
    public class OnDiedEffect : Effect
    {
        [SerializeField] private Creature.EventTypes EffectName;
        public override Creature.EventTypes Name => EffectName;

        public override void OnStart(Transform Parent)
        {
        }

        public override void Play()
        {
        }
    }
}