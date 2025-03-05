using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Core.UI
{
    public class UIManager : MonoBehaviour
    {
        [Zenject.Inject] Zenject.SignalBus Signaller;

        [SerializeField] private TextMeshProUGUI OnPreysKilled;
        [SerializeField] private TextMeshProUGUI OnPredatorsKilled;

        private void Awake()
        {
            Signaller.Subscribe<Statistics>(OnUpdateStatistics);
        }

        private void OnUpdateStatistics(Statistics Stats)
        {
            OnPreysKilled.text = Stats.GetKilledCreature(FoodChainTypes.Prey).ToString();
            OnPredatorsKilled.text = Stats.GetKilledCreature(FoodChainTypes.Predator).ToString();
        }
    }
}