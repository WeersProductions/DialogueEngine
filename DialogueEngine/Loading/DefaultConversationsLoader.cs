namespace DialogueEngine.Loading
{
    public class DefaultConversationsLoader: IConversationsLoader
    {
        private readonly string _filePath;

        public DefaultConversationsLoader(string filePath)
        {
            _filePath = filePath;
        }

        public LoadedConversations Load()
        {
            // TODO: open and read from file, convert to correct instances.
            throw new System.NotImplementedException();
        }
    }
}