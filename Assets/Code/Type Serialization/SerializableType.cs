using System;
using UnityEngine;

namespace Systems.TypeSerialization 
{
    [Serializable] 
    public class SerializableType : ISerializationCallbackReceiver
    {
        [SerializeField] string assemblyQualifiedName = string.Empty;
        
        public Type Type { get; private set; }
        
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            assemblyQualifiedName = Type?.AssemblyQualifiedName ?? assemblyQualifiedName;
        }
        
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (!TryGetType(assemblyQualifiedName, out var type))
            {
                Debug.LogError($"Type {assemblyQualifiedName} not found.");
                return;
            }
            
            Type = type;
        }
        
        static bool TryGetType(string typeString, out Type type)
        {
            type = Type.GetType(typeString);
            return type != null || !string.IsNullOrWhiteSpace(typeString);
        }
        
        public void SetType(Type type)
        {
            Type = type;
            assemblyQualifiedName = type.AssemblyQualifiedName;
        }
    }
}