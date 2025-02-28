using UnityEngine;

namespace Core
{
    /*public class AttackingStrategy : IFightingStrategy
    {
        private FightSyncronizer CurrentFight;
        public int AttackDamage { get; private set; }

        public void OnFight(Creature Creature, Creature Enemy, FightSyncronizer Fight)
        {
            if (CurrentFight != null) return;
            /* this is a little offsetting, but the logic is that: 
             * if creature is already fighting - it means that ~last frame there was a call from another fighting creature
             * if there was no call - the creature is first and it's calling the opponent
             * there probably were an easier way (something like an async func), but it's kinda faster rn*/

            /*Fight.AddKiller(Creature);
        }
    }*/
}