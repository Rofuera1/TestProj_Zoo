using System.Collections;
using UnityEngine;

namespace Core
{
    public class CreaturesManager : MonoBehaviour
    {
        [SerializeField] private float SecondsDelay; // TODO: Move to scriptables
        [Zenject.Inject] private CreaturesFactory Factory;

        private void Awake()
        {
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
            Factory.CreateCreature((CreatureTypes.Snake));
        }
    }
}