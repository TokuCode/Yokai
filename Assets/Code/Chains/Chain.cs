using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Systems.EventBus
{
    public static class Chain<T> where T : IEvent
    {
        static readonly List<IChainBinding<T>> bindings = new List<IChainBinding<T>>();
        
        public static void Register(IChainBinding<T> binding) => bindings.Add(binding);
        public static void Deregister(IChainBinding<T> binding) => bindings.Remove(binding);
        
        public static T Proccess(T @event)
        {
            var orderedBindings = bindings.OrderByDescending(b => b.priority);
            
            T currentEvent = @event;
            foreach (var binding in orderedBindings)
            {
                currentEvent = binding.Modify(currentEvent);
            }

            return currentEvent;
        }
        
        static void Clear()
        {
            Debug.Log($"Clearing {typeof(T).Name} Chain bindings...");
            bindings.Clear();
        }
    }
}