using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class FightReferee : MonoBehaviour
    {
        private Dictionary<Creature, FightSession> ActiveFights = new Dictionary<Creature, FightSession>();

        public FightSession GetCurrentFight(Creature Creature)
        {
            if (!ActiveFights.ContainsKey(Creature)) return new FightSession();

            return ActiveFights[Creature];
        }

        public void AddCreatureToFight(Creature Creature)
        {

        }
    }

    public class FightSession
    {
        private List<Creature> WannaKill;
        private List<Creature> WannaFlee;

        public Action<Creature> OnAlive;
        public Action<Creature> OnDead;

        public FightSession()
        {
            WannaKill = new List<Creature>();
            WannaFlee = new List<Creature>();
        }

        public void AddKiller(Creature Creature)
        {
            WannaKill.Add(Creature);
            TryEndFight();
        }

        public void AddFleer(Creature Creature) // flier? fleer? flier? i wish google existed...
        {
            WannaFlee.Add(Creature);
            TryEndFight();
        }

        private void TryEndFight()
        {
            if (WannaKill.Count + WannaFlee.Count < 2) return;

            bool anyKillers = WannaKill.Count > 0;
            if (anyKillers)
                NotPeaceful();
            else
                Peaceful();
        }

        private void NotPeaceful() // Fully random func - can be changed, if needed
        {
            int WinnerID = UnityEngine.Random.Range(0, WannaKill.Count);
            Creature Winner = WannaKill[WinnerID];
            WannaKill.RemoveAt(WinnerID);

            foreach (Creature Creature in WannaKill)
                OnDead?.Invoke(Creature);
            foreach (Creature Creature in WannaFlee)
                OnDead?.Invoke(Creature);

            OnAlive?.Invoke(Winner); // It can be called at start, in the middle, or at the end - doesn't really matter (fyi)
        }

        private void Peaceful()
        {
            foreach (Creature Creature in WannaFlee)
                OnAlive?.Invoke(Creature);
        }
    }
}
