using System;
using UnityEngine;
using UnityEditor;
using Systems.TypeSerialization;
using System.Linq;
using Systems.EventBus;

namespace Systems.Yokai
{
    [CustomPropertyDrawer(typeof(SingleEffect))]
    public class SingleEffectDrawer : PropertyDrawer
    {
        private string[] eventTypeNames, eventTypeFullNames;
        private string selectedEvent;
        private string[] effectTypeNames, effectTypeFullNames;

        private void LoadEventTypes()
        {
            var filteredEvents = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => Filter(t, typeof(IEvent)))
                .ToArray();
            
            eventTypeNames = filteredEvents.Select(t => t.ReflectedType == null ? t.Name : $"{t.ReflectedType.Name}.{t.Name}").ToArray();
            eventTypeFullNames = filteredEvents.Select(t => t.AssemblyQualifiedName).ToArray();
        }

        private void LoadEffectTypes()
        {
            if (string.IsNullOrWhiteSpace(selectedEvent))
            {
                effectTypeNames = new string[0];
                effectTypeFullNames = new string[0];
                return;
            }

            var eventType = Type.GetType(selectedEvent);
            var effectType = typeof(BaseEffect<>).MakeGenericType(eventType);
            
            var filteredEffects = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => Filter(t, effectType))
                .ToArray();
            
            effectTypeNames = filteredEffects.Select(t => t.ReflectedType == null ? t.Name : $"{t.ReflectedType.Name}.{t.Name}").ToArray();
            effectTypeFullNames = filteredEffects.Select(t => t.AssemblyQualifiedName).ToArray();
        }

        static bool Filter(Type type, Type filterType)
        {
            return !type.IsAbstract &&
                   !type.IsInterface &&
                   !type.IsGenericType &&
                   type.InheritsOrImplements(filterType);
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            LoadEventTypes();
            var eventProperty = property.FindPropertyRelative("Event");
            
            if (string.IsNullOrEmpty(eventProperty.stringValue))
            {
                eventProperty.stringValue = eventTypeFullNames.First();
                property.serializedObject.ApplyModifiedProperties();
            }
            
            var currentEventIndex = Array.IndexOf(eventTypeFullNames, eventProperty.stringValue);
            var selectedEventIndex = EditorGUILayout.Popup(label.text, currentEventIndex, eventTypeNames);
            
            if (selectedEventIndex >= 0 && selectedEventIndex != currentEventIndex)
            {
                eventProperty.stringValue = eventTypeFullNames[selectedEventIndex];
                property.serializedObject.ApplyModifiedProperties();
                selectedEvent = eventTypeFullNames[selectedEventIndex];
            }
            
            LoadEffectTypes();
            if (effectTypeNames.Length == 0) return;
            
            var effectProperty = property.FindPropertyRelative("Effect");
            if (string.IsNullOrEmpty(effectProperty.stringValue))
            {
                effectProperty.stringValue = effectTypeFullNames.First();
                property.serializedObject.ApplyModifiedProperties();
            }
            
            var currentEffectIndex = Array.IndexOf(effectTypeFullNames, effectProperty.stringValue);
            var selectedEffectIndex = EditorGUILayout.Popup("Effect Type", currentEffectIndex, effectTypeNames);
            
            if (selectedEffectIndex >= 0 && selectedEffectIndex != currentEffectIndex)
            {
                effectProperty.stringValue = effectTypeFullNames[selectedEffectIndex];
                property.serializedObject.ApplyModifiedProperties();
            }
        }
    }
}