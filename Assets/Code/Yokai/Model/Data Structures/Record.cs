namespace Systems.Yokai
{
    public interface IRecorder<T> where T : IRecord
    {
        void Record();
        void Play(T record);
    }

    public interface IRecord
    {
    }
}