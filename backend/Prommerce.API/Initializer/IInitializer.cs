namespace Prommerce.API.Initializer
{
    public interface IInitializer
    {
        Task Initialise();

        int Priority { get; }
    }
}