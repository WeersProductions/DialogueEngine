namespace DialogueEngine.Messages
{
    public class Message: IMessage
    {
        private StandardDataType[] _requiredData;
        private readonly int _id;
        private readonly string _rawString;

        public Message(string rawString, int id)
        {
            _rawString = rawString;
            _id = id;
        }

        public ParsedMessage Render(IMessageParser messageParser)
        {
            // TODO: render message.
            return messageParser.Parse(_rawString);
        }

        protected bool Equals(Message other)
        {
            return _id == other._id && _rawString == other._rawString;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Message) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_id * 397) ^ (_rawString != null ? _rawString.GetHashCode() : 0);
            }
        }
    }
}