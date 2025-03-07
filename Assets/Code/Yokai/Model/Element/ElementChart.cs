using System.Collections.Generic;
using UnityEngine;

namespace Systems.Yokai 
{
    public static class ElementChart 
    {
        private static Dictionary<(Element, Element), float> Multipliers;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void Initialize()
        {
            Multipliers = new Dictionary<(Element, Element), float>();
            
            var allElements = Resources.LoadAll<Element>("");
            
            foreach (var element in allElements)
            {
                AddElement(element);
            }
        }
        
        static void AddElement(Element element)
        {
            AddMultiplier(element, element.StrongAgainst, 2);
            AddMultiplier(element, element.WeakAgainst, 0.5f);
            AddMultiplier(element, element.ImmuneTo, 0);
            AddMultiplier(element, element.ResistantTo, 0.5f, false);
            AddMultiplier(element, element.WeakTo, 2, false);
            AddMultiplier(element, element.InmuneAgainst, 0, false);
        }
        
        static void AddMultiplier(Element origin, Element[] targets, float multiplier, bool originIsAttacker = true)
        {
            foreach (var target in targets)
            {
                var elementChartCoordinate = originIsAttacker ? (origin, target) : (target, origin);
                Multipliers.TryAdd(elementChartCoordinate, multiplier);
            }
        }

        public static float GetMultiplier(Element attacker, Element defender)
        {
            if (Multipliers == null)
            {
                return 1;
            }

            if (Multipliers.TryGetValue((attacker, defender), out var multiplier))
            {
                return multiplier;
            }

            return 1;
        }
    }
}