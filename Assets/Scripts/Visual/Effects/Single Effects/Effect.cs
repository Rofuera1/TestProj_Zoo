namespace Core.Effects
{
    public abstract class Effect: UnityEngine.MonoBehaviour
    {
        public abstract Creature.EventTypes Name { get; }
        public abstract void OnStart(UnityEngine.Transform Parent);
        public abstract void Play();
    }
}