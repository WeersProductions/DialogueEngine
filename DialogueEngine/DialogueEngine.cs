using System.Threading.Tasks;
using DialogueEngine.Loading;
using DialogueEngine.Messages;
using DialogueEngine.ToExternal;

namespace DialogueEngine
{
    public class DialogueEngine
    {
        private readonly IConversationsLoader _conversationsLoader;
        private LoadedConversations _loadedConversations;
        /// <summary>
        /// If true, the engine is loaded.
        /// </summary>
        private bool _loaded;
        
        public DialogueEngine(IConversationsLoader conversationsLoader, bool loadImmediately = false)
        {
            _conversationsLoader = conversationsLoader;

            if (loadImmediately)
            {
                Load();
            }
        }

        public bool Load()
        {
            bool success = _conversationsLoader.Load(out var loadedConversations);
            _loadedConversations = loadedConversations;
            _loaded = true;
            
            return success;
        }

        /// <summary>
        /// Ensure all required data is loaded.
        /// </summary>
        private void EnsureLoaded()
        {
            if (_loaded)
            {
                return;
            }

            Load();
        }
        
        public ConversationElement GetConversationElement(int conversationStepId)
        {
            EnsureLoaded();

            if (!_loadedConversations.Conversations.TryGetValue(conversationStepId, out var conversationStep))
            {
                return new ConversationElement();
            }

            var message = conversationStep.ConversationItem.MessageProvider.GetMessage();
            var renderInfo = conversationStep.ConversationItem.ConversationItemProvider.Render(conversationStep.NextStep);
            
            return new ConversationElement(message, renderInfo);
        }
    }
}