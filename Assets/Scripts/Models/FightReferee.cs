using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class FightReferee : MonoBehaviour // Helping with syncronyzing all fights between creatures; No need to use async/hard connections between enemies, or debug the collisions
    {
        private Dictionary<string, FightSession> ActiveFights = new Dictionary<string, FightSession>();

        public FightSession GetCurrentFight(Creature Creature, Creature Enemy)
        {
            string FightKey = ReturnSortedString(Creature.gameObject, Enemy.gameObject);
            if (!ActiveFights.ContainsKey(FightKey))
            {
                FightSession NewSession = new FightSession(FightKey);

                NewSession.OnEnded += () => { OnFightEnded(NewSession); }; // Technically, there's no need to unsubsctibe - the object will be destroyed, and if not - there'll be exception

                ActiveFights.Add(FightKey, NewSession);

                return NewSession;
            }

            return ActiveFights[FightKey];
        }

        public void OnFightEnded(FightSession Session)
        {
            if (!ActiveFights.ContainsKey(Session.FightID)) throw new Exception("No fight with " + Session.FightID + " found");
            ActiveFights.Remove(Session.FightID);
        }

        private string ReturnSortedString(GameObject F, GameObject S) // Creating unique (and same) key for each two objects to store in the dictionary
        {
            int F_ID = F.GetInstanceID();
            int S_ID = S.GetInstanceID();

            return Math.Min(F_ID, S_ID).ToString() + Math.Max(F_ID, S_ID).ToString();
        }
    }

    public class FightSession                                   // A way to keep collisions synchronized (For two objects only!). Could be improved
    {
        public string FightID { get; private set; }
        public List<Creature> WannaKill { get; private set; }   // This (List) is just a nice way to keep it logical - but there's always only two involvants in the session!
        public List<Creature> WannaFlee { get; private set; }

        public event Action<Creature> OnAlive;
        public event Action<Creature> OnAliveAndKilled;
        public event Action<Creature> OnDead;
        public event Action OnEnded;

        public FightSession(string ID)
        {
            FightID = ID;
            WannaKill = new List<Creature>();
            WannaFlee = new List<Creature>();
        }

        ///<summary>
        /// Note that after adding last involvant, actions OnAlive, OnAliveAndKilled, OnDead and OnEnded will be called immediately. Better to subscribe before calling this func
        ///</summary>
        public void AddKiller(Creature Creature)
        {
            WannaKill.Add(Creature);
            TryEndFight();
        }

        ///<summary>
        /// Note that after adding last involvant, actions OnAlive, OnAliveAndKilled, OnDead and OnEnded will be called immediately. Better to subscribe before calling this func
        ///</summary>
        public void AddFleer(Creature Creature) // flier? fleer? flier? i wish google existed...
        {
            WannaFlee.Add(Creature);
            TryEndFight();
        }

        private void TryEndFight() // meaning "try to resolve conflict": if there's already two involvants, then via some logic, tell each one of them what happened: they died, fleed, or alive (and killed someone)
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

            OnAliveAndKilled?.Invoke(Winner); // It can be called at start, in the middle, or at the end - doesn't really matter (fyi)
            OnEnded?.Invoke();
        }

        private void Peaceful()
        {
            foreach (Creature Creature in WannaFlee)
                OnAlive?.Invoke(Creature);
            OnEnded?.Invoke();
        }
    }
}
