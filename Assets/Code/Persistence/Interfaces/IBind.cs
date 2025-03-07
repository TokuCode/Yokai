﻿using Systems.Id;

namespace Systems.Persistence
{
    public interface IBind<TData> where TData : ISaveable
    {
        SerializableGuid Id { get; set; }
        void Bind(TData data);
    }
}