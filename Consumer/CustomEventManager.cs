namespace ConsumerWindowsService
{
    public class CustomEventManager : EventArgs
    {
        public string Message { get; set; }
        public CustomEventManager(string message)
        {
            Message = message;
        }

        public delegate void EventHandler(object sender, EventArgs e);
    }

    public class Publisher
    {
        public event EventHandler<CustomEventManager> RaiseCustomEvent;

        protected virtual void OnRaiseCustomEvent(CustomEventManager e)
        {
            RaiseCustomEvent?.Invoke(this, e);
        }
    }

    public class Subscriber
    {
        private readonly string _id;

        public Subscriber(string id, Publisher pub)
        {
            _id = id;
            pub.RaiseCustomEvent += HandleCustomEvent;
        }

        public void HandleCustomEvent(object? sender, CustomEventManager e)
        {
            Console.WriteLine("Evento notificado");
        }
    }
}
