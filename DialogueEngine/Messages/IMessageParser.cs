using DialogueEngine.Messages;

namespace DialogueEngine
{
    /// <summary>
    /// Responsible to parse raw message text to something easily rendered. 
    /// </summary>
    public interface IMessageParser
    {
        ParsedMessage Parse(string rawString);
    }
}