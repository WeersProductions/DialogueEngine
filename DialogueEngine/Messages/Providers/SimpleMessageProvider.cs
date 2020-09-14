namespace DialogueEngine.Messages.Providers
{
    /// <summary>
    /// Always returns the same message.
    /// </summary>
    public class SimpleMessageProvider: IMessageProvider
    {
        private readonly Message _message;

        public SimpleMessageProvider(Message message)
        {
            _message = message;
        }

        public Message GetMessage()
        {
            return _message;
        }
    }
}