using System;
using Systems.EventBus;
using Systems.TypeSerialization;
using UnityEngine;

namespace Systems.Yokai
{
    [Serializable]
    public class SingleEffect
    {
        [HideInInspector] public SerializableType Event;
        [HideInInspector] public SerializableType Effect;
    }
}