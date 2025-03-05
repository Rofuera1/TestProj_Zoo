using System.Collections;
using UnityEngine;

namespace Core
{
    public class CreaturesManager : MonoBehaviour
    {
        [SerializeField] private float SecondsDelay; // TODO: Move to scriptables

        [Zenject.Inject] private CreaturesFactory Factory;
        [Zenject.Inject] private Zenject.SignalBus Signaller;

        private void Awake()
        {
            Signaller.Subscribe<Creature.CreatureAction>(CreatureAction);

            StartCoroutine(OnCreateLooped());
        }

        private IEnumerator OnCreateLooped()
        {
            while(true)
            {
                CreateObject();

                yield return new WaitForSeconds(SecondsDelay);
            }
        }

        private void CreateObject()
        {
            Factory.CreateCreature((CreaturesFactory.CreatureTypes)Random.Range(0, 2));
        }

        private void CreatureAction(Creature.CreatureAction Action)
        {
            switch(Action.Action)
            {
                case Creature.EventTypes.OnDied:
                    CreatureDied(Action.Creature);
                    break;
            }
        }

        private void CreatureDied(Creature Creature)
        {
            Factory.CreatureDied(Creature);
        }
    }
}