namespace Systems.EventBus
{
    public interface IChainBinding<T> where T : IEvent
    {
        int priority { get; }
        T Modify(T value);
    }
}