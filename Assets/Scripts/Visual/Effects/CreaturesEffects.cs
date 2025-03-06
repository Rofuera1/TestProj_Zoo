using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class CreaturesEffects : MonoBehaviour
    {
        [Zenject.Inject] private Zenject.SignalBus Signaller;

        [SerializeField] private Effects.Effect[] EffectsPrefabs;
        [SerializeField] private Transform EffectsParent;

        private Dictionary<Creature.EventTypes, Effects.Effect> EffectsDictionary = new Dictionary<Creature.EventTypes, Effects.Effect>();

        private void Awake()
        {
            Signaller.Subscribe<Creature.CreatureAction>(OnCreatureEffect);

            SortEffects();
        }

        private void SortEffects()
        {
            foreach(var Effect in EffectsPrefabs)
            {
                if (EffectsDictionary.ContainsKey(Effect.Name)) throw new System.Exception("Two effects with the same name: " + Effect.name);

                EffectsDictionary.Add(Effect.Name, Effect);
            }
        }

        private void OnCreatureEffect(Creature.CreatureAction Action)
        {
            if(!EffectsDictionary.ContainsKey(Action.Action))
            {
                Debug.LogWarning("No such effect: " + Action.Action);
                return;
            }

            CreateEffect(EffectsDictionary[Action.Action], Action.Creature);
        }

        private void CreateEffect(Effects.Effect Effect, Creature Creature)
        {
            Effects.Effect NewEffect = Instantiate(Effect,
                                                    Vector3.zero,
                                                    Quaternion.identity,
                                                    EffectsParent);
            NewEffect.OnStart(Creature.transform);
            NewEffect.Play();
        }
    }
}