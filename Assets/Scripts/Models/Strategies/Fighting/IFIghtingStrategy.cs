using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface IFightingStrategy
    {
        public void OnFight(Creature Creature, Creature Enemy, FightSyncronizer Fight);
    }

    public class FightSyncronizer
    {
        
    }
}