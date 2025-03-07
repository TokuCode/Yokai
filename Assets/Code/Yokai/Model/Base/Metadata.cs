using System;
using Systems.Id;
using UnityEngine;

namespace Systems.Yokai
{
    [Serializable]
    public class Metadata
    {
        public string Name;
        public string Description;
        public Sprite Image;
        public SerializableGuid Id = SerializableGuid.NewGuid();
        public SerializableGuid OwnerId;
    }
}