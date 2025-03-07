using System;
using System.Collections.Generic;

namespace Systems.Yokai
{
    public class StatGroup
    {
        public readonly IReadOnlyList<string> statNames;
        public Dictionary<string, int> stats;

        public StatGroup(string[] statNames, int defaultValue = 0) 
        {
            this.statNames = statNames;
            stats = new Dictionary<string, int>();
            foreach (var name in statNames)
            {
                stats.Add(name, defaultValue);
            }
        }
        
        public int this[string statName]
        {
            get => stats[statName];
            set => stats[statName] = value;
        }
    }
}