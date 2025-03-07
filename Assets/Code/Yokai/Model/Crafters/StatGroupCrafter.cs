using System;

namespace Systems.Yokai
{
    [Serializable]
    public class StatGroupCrafter : ICrafter<StatGroup>
    {
        [Serializable]
        public struct Stat
        {
            public string name;
            public int value;
        }
        
        public Stat[] stats;
        
        public StatGroup Craft()
        {
            var statNames = new string[stats.Length];
            var statValues = new int[stats.Length];
            
            for (var i = 0; i < stats.Length; i++)
            {
                statNames[i] = stats[i].name;
                statValues[i] = stats[i].value;
            }
            
            var statGroup = new StatGroup(statNames);
            
            for (var i = 0; i < stats.Length; i++)
            {
                statGroup[statNames[i]] = statValues[i];
            }
            
            return statGroup;
        }
    }
}