using System.Collections.Generic;
using DialogueEngine.Conversation;

namespace DialogueEngine.Loading
{
    public class LoadedConversations
    {
        public readonly Dictionary<int, ConversationStep> Conversations;

        public LoadedConversations(Dictionary<int, ConversationStep> conversations)
        {
            Conversations = conversations;
        }
    }
}