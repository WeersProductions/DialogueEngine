namespace DialogueEngine.Messages
{
    public interface IMessage
    {
        ParsedMessage Render(IMessageParser messageParser);
    }
}