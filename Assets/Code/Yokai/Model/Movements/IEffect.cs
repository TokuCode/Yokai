using System;
using Systems.EventBus;
using Systems.Id;
using Systems.Yokai;

namespace Systems.Yokai
{
    public interface IEffect<T> where T : IEvent
    {
        void Effect(T evt);
    }
    
    public abstract class BaseEffect<T> : IEffect<T>, IDisposable where T : IEvent
    {
        //Id
        public SerializableGuid Id;
        public SerializableGuid OwnerId;
        
        // Stats
        public StatGroup stats;

        // Brain
        public IBrain brain;
        
        // Effects
        public EventBinding<T> Binding;
        
        public abstract void Effect(T evt);

        public void Initialize()
        {
            Binding = new EventBinding<T>(Effect);
            EventBus<T>.Register(Binding);
            Id = SerializableGuid.NewGuid();
        }

        public void Dispose()
        {
            EventBus<T>.Deregister(Binding);
        }
    }
    
    public class EffectBuilder<T, Tevent> where T : BaseEffect<Tevent>, new() where Tevent : IEvent
    {
        public SerializableGuid OwnerId;
        
        public StatGroup stats;
            
        public IBrain brain;

        public EffectBuilder<T, Tevent> WithStats(StatGroup stats)
        {
            this.stats = stats;

            return this;
        }
            
        public EffectBuilder<T, Tevent> WithRegister(IBrain brain)
        {
            this.brain = brain;

            return this;
        }

        public EffectBuilder<T, Tevent> WithOwner(SerializableGuid OwnerId)
        {
            this.OwnerId = OwnerId;

            return this;
        }
            
        public T Build()
        {
            T effect = new T
            {
                OwnerId = OwnerId,
                Id = SerializableGuid.NewGuid(),
                
                stats = stats,
                
                brain = brain
            };
            
            effect.Initialize();

            return effect;
        }
    } 
}