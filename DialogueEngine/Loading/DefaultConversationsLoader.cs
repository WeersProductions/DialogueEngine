namespace DialogueEngine.Loading
{
    public class DefaultConversationsLoader: IConversationsLoader
    {
        private readonly string _filePath;

        public DefaultConversationsLoader(string filePath)
        {
            _filePath = filePath;
        }

        public bool Load(out LoadedConversations loadedConversations)
        {
            // TODO: open and read from file, convert to correct instances.
            // Should first initialize all messages, then conversationItems, then conversationSteps.
            throw new System.NotImplementedException();
        }
    }
}