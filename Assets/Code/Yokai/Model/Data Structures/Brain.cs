using System;
using System.Collections.Generic;

namespace Systems.Yokai
{
    public interface IBrain : ICloneable
    {
        string ComposeKey(params string[] direction);
        string[] DecomposeKey(string key);
        bool Register(string key, int value);
        void Unregister(string key);
        int GetRegister(string key);
        void AddRegister(string key, int value);
        void SubstractRegister(string key, int value);
        void IncrementRegister(string key);
        void DecrementRegister(string key);
        void ClearRegisters();
    }

    public class Brain : IBrain
    {
        private const string SEPARATOR = ".";
        
        private Dictionary<string, int> register;

        public int this[string key]
        {
            get => GetRegister(key);
            set => Register(key, value);
        } 
        
        public string ComposeKey(params string[] direction) => string.Join(SEPARATOR, direction);
        public string[] DecomposeKey(string key) => key.Split(SEPARATOR);
        
        public bool Register(string key, int value)
        {
            if (register == null)
            {
                register = new Dictionary<string, int>();
            }

            return register.TryAdd(key, value);
        }
        
        public void Unregister(string key)
        {
            if (!register.ContainsKey(key)) return;
            
            register.Remove(key);
        }
        
        public int GetRegister(string key)
        {
            if (!register.ContainsKey(key)) return 0;
            
            return register[key];
        }
        
        public void AddRegister(string key, int value)
        {
            if (!register.ContainsKey(key)) return;
            
            register[key] += value;
        }
        
        public void SubstractRegister(string key, int value)
        {
            AddRegister(key, -value);
        }
        
        public void IncrementRegister(string key)
        {
            AddRegister(key, 1);
        }
        
        public void DecrementRegister(string key)
        {
            AddRegister(key, -1);
        }
        
        public void ClearRegisters()
        {
            register.Clear();
        }

        public object Clone()
        {
            Brain clone = new Brain();
            foreach (KeyValuePair<string, int> pair in register)
            {
                clone.Register(pair.Key, pair.Value);
            }
            return clone;
        }
    }
}