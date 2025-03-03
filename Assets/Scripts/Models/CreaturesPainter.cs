using UnityEngine;

namespace Core
{
    public class CreaturesPainter : MonoBehaviour
    {
        [SerializeField] private Material PreyMaterial;         // An easy implementation for an easy game
        [SerializeField] private Material PredatorMaterial;

        public void PaintCreatureOnStart(Creature Creature)
        {
            switch(Creature.CreatureType)
            {
                case FoodChainTypes.Prey:
                    Creature.GetComponent<MeshRenderer>().material = PreyMaterial;
                break;
                case FoodChainTypes.Predator:
                    Creature.GetComponent<MeshRenderer>().material = PredatorMaterial;
                break;
            }
        }
    }
}