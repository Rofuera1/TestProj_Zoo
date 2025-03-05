using System.Collections;
using UnityEngine;

namespace Core.Effects
{
    public class OnKillEffect : Effect
    {
        [SerializeField] private Creature.EventTypes EffectName;
        [SerializeField] private float LastingTime;

        public override Creature.EventTypes Name { get => EffectName; }
        private Transform EffectAt;

        private Vector3 NeededPosition;

        public override void OnStart(Transform Parent)
        {
            EffectAt = Parent;
            transform.position = Parent.position;
        }

        public override void Play()
        {
            gameObject.SetActive(true);

            StartCoroutine(StickToParent());
        }

        private IEnumerator StickToParent()
        {
            float t = 0f;
            Vector3 refVector = Vector3.zero;
            StartCoroutine(LerpFromParentDown(0.2f));

            while(t < LastingTime)
            {
                t += Time.deltaTime;

                transform.position = Vector3.SmoothDamp(transform.position, NeededPosition, ref refVector, 0.2f);

                yield return null;
            }

            Destroy(gameObject);
        }

        private IEnumerator LerpFromParentDown(float time)
        {
            float t = 0f;

            while(t < time)
            {
                t += Time.deltaTime;

                NeededPosition = Vector3.Lerp(EffectAt.position, EffectAt.position + Vector3.down, t / time);

                yield return null;
            }
        }
    }
}