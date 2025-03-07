using UnityEngine;

namespace Systems.Yokai
{
    public class Element : Queryable
    {
        [Header("Element Chart")]
        public Element[] StrongAgainst; //Super effective when attacking these types
        public Element[] WeakAgainst; //Not very effective when attacking these types
        public Element[] ImmuneTo; //No effect when attacking these types
        public Element[] ResistantTo; //Takes less damage when attacked by these types
        public Element[] WeakTo; //Takes more damage when attacked by these types
        public Element[] InmuneAgainst; //No effect when attacked by these types  
    }
}