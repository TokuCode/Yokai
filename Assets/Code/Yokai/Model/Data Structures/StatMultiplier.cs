using UnityEngine;

namespace Systems.Yokai
{
    public interface IStatMultiplier
    {
        int Scale { get; }
        
        bool SetMultiplier(int value);
        bool AddMultiplier(int value);
        bool RemoveMultiplier(int value);
        bool IncrementMultiplier();
        bool DecrementMultiplier();
        void ResetMultiplier();
        float GetMultiplier();
    }
    
    public abstract class StatMultiplier : IStatMultiplier
    {
        private const int MAX_SCALE = 6;
        
        private int scale;
        public int Scale => scale;

        public StatMultiplier(int startValue = 0)
        {
            SetMultiplier(startValue);
        }

        private bool Normalize()
        {
            if (scale > MAX_SCALE)
            {
                scale = MAX_SCALE;
                return true;
            }
            
            if (scale < -MAX_SCALE)
            {
                scale = -MAX_SCALE;
                return true;
            }
            
            return false;
        }
        
        public bool SetMultiplier(int value)
        {
            scale = value;
            return Normalize();
        }
        
        public bool AddMultiplier(int value)
        {
            if(value < 0) return RemoveMultiplier(-value);
            
            scale += value;
            return Normalize();
        }
        
        public bool RemoveMultiplier(int value)
        {
            if(value < 0) return AddMultiplier(-value);
            
            scale -= value;
            return Normalize();
        }
        
        public bool IncrementMultiplier()
        {
            scale++;
            return Normalize();
        }
        
        public bool DecrementMultiplier()
        {
            scale--;
            return Normalize();
        }

        public void ResetMultiplier()
        {
            scale = 0;
        }

        public abstract float GetMultiplier();
    }
}