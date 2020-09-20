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

        public bool Loaded => _loaded;

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
            var loadResult = _conversationsLoader.Load();
            loadResult.Wait();
            var result = loadResult.Result;
            _loadedConversations = result.Result;
            _loaded = true;

            return result.Success;
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

            if (_loadedConversations == null || !_loadedConversations.Conversations.TryGetValue(conversationStepId, out var conversationStep))
            {
                return new ConversationElement();
            }

            var message = conversationStep.ConversationItem.MessageProvider.GetMessage();
            var renderInfo = conversationStep.ConversationItem.ConversationItemProvider.Render(conversationStep.NextStep);
            
            return new ConversationElement(message, renderInfo);
        }
    }
}