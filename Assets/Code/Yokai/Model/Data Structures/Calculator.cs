namespace Systems.Yokai 
{
    public interface ICalculator<Tin, Tout>
    {
        Tout Calculate(Tin input);
    }
}