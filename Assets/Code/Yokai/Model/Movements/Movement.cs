using UnityEngine;
using Systems.TypeSerialization;
using Systems.EventBus;

namespace Systems.Yokai
{
    [CreateAssetMenu(fileName = "New Movement", menuName = "Monster Duel/Movement")]
    public class Movement : Queryable
    {
        [Header("Stats")] 
        public StatGroupCrafter stats;
        
        [Header("Effects")]
        public SingleEffect[] effects;
        
        [Header("Tags")]
        public TagsCrafter tags;
    }
}