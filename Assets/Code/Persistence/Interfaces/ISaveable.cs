﻿using Systems.Id;

namespace Systems
{
    public interface ISaveable
    {
        SerializableGuid Id { get; set; }
    }
}