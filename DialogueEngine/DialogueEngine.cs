using DialogueEngine.Loading;

namespace DialogueEngine
{
    public class DialogueEngine
    {
        private readonly IConversationsLoader _conversationsLoader;
        
        public DialogueEngine(IConversationsLoader conversationsLoader)
        {
            _conversationsLoader = conversationsLoader;
        }
    }
}