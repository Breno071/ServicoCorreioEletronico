namespace Producer
{
    public interface IProducer
    {
        Task Send(object message);
    }
}
