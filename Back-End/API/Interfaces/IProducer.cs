namespace API.Interfaces
{
    public interface IProducer
    {
        Task Send(object message);
    }
}
