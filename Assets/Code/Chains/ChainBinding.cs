using System;

namespace Systems.EventBus
{
    public class ChainBinding<T> where T : IEvent
    {
        public int priority { get; private set; }
        public Func<T, T> modifyFunction { get; private set; }
        
        public ChainBinding(int priority, Func<T, T> modifyFunction)
        {
            this.priority = priority;
            this.modifyFunction = modifyFunction;
        }
        
        public T Modify(T value) => modifyFunction(value);
    }
}