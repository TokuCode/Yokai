using System;
using UnityEngine;

namespace Systems.Id
{
    [Serializable]
    public class SerializableGuid
    {
        [HideInInspector, SerializeField] public uint Part1;
        [HideInInspector, SerializeField] public uint Part2;
        [HideInInspector, SerializeField] public uint Part3;
        [HideInInspector, SerializeField] public uint Part4;
    
        public static SerializableGuid NewGuid() => new SerializableGuid(Guid.NewGuid());
        public static SerializableGuid Empty() => new SerializableGuid(0, 0, 0, 0);
        
        public SerializableGuid(uint part1, uint part2, uint part3, uint part4)
        {
            Part1 = part1;
            Part2 = part2;
            Part3 = part3;
            Part4 = part4;
        }
        
        public SerializableGuid(Guid guid)
        {
            byte[] bytes = guid.ToByteArray();
            Part1 = BitConverter.ToUInt32(bytes, 0);
            Part2 = BitConverter.ToUInt32(bytes, 4);
            Part3 = BitConverter.ToUInt32(bytes, 8);
            Part4 = BitConverter.ToUInt32(bytes, 12);
        }
        
        public Guid ToGuid()
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(Part1).CopyTo(bytes, 0);
            BitConverter.GetBytes(Part2).CopyTo(bytes, 4);
            BitConverter.GetBytes(Part3).CopyTo(bytes, 8);
            BitConverter.GetBytes(Part4).CopyTo(bytes, 12);
            return new Guid(bytes);
        }
    
        public string ToHexString()
        {
            return $"{Part1:x8}{Part2:x8}{Part3:x8}{Part4:x8}";
        }
    
        public override string ToString()
        {
            return ToHexString();
        }
        
        public override bool Equals(object obj)
        {
            return obj is SerializableGuid guid && Equals(guid);
        }
        
        public bool Equals(SerializableGuid other)
        {
            return Part1 == other.Part1 && Part2 == other.Part2 && Part3 == other.Part3 && Part4 == other.Part4;
        }
    }
}