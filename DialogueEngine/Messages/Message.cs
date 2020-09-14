namespace DialogueEngine.Messages
{
    public class Message: IMessage
    {
        private StandardDataType[] _requiredData;
        private int _id;
        private string _rawString;

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
    }
}