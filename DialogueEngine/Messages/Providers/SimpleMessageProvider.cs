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

        protected bool Equals(SimpleMessageProvider other)
        {
            return Equals(_message, other._message);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SimpleMessageProvider) obj);
        }

        public override int GetHashCode()
        {
            return (_message != null ? _message.GetHashCode() : 0);
        }
    }
}