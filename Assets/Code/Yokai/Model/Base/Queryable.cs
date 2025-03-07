using Systems.Id;
using UnityEngine;

namespace Systems.Yokai
{
    public abstract class Queryable : ScriptableObject, IQueryable
    {
        public Metadata Metadata => metadata;
        public SerializableGuid Id => metadata.Id;
        
        [Header("Metadata")]
        [SerializeField] private Metadata metadata;
    }
}