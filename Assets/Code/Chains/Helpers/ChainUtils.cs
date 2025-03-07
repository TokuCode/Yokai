using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Systems.EventBus
{
    public class ChainUtils
    {
        public static IReadOnlyList<Type> EventTypes { get; set; }
        public static IReadOnlyList<Type> ChainTypes { get; set; }
        
#if UNITY_EDITOR
        public static PlayModeStateChange PlayModeState { get; set; }
        
        [InitializeOnLoadMethod]
        public static void InitializeEditor()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }
        
        static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            PlayModeState = state;
            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                ClearAllChains();
            }
        }
#endif
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            EventTypes = PredefinedAssemblyUtil.GetTypes(typeof(IEvent));
            ChainTypes = InitializeAllChains();
        }

        static List<Type> InitializeAllChains()
        {
            List<Type> chainTypes = new List<Type>();

            var typedef = typeof(Chain<>);
            foreach (var eventType in EventTypes)
            {
                var chainType = typedef.MakeGenericType(eventType);
                chainTypes.Add(chainType);
            }
            
            return chainTypes;
        }
        
        public static void ClearAllChains()
        {
            Debug.Log("Clearing all chains...");
            for (int i = 0; i < ChainTypes.Count; i++)
            {
                var busType = ChainTypes[i];
                var clearMethod = busType.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
                clearMethod.Invoke(null, null);
            }
        }
    }
}