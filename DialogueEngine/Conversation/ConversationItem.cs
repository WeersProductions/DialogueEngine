using DialogueEngine.Conversation.ConversationItemProviders;
using DialogueEngine.Messages.Providers;

namespace DialogueEngine.Conversation
{
    public class ConversationItem
    {
        public readonly int Id;
        public readonly IMessageProvider MessageProvider;
        public readonly IConversationItemProvider ConversationItemProvider;

        public ConversationItem(int id, IMessageProvider messageProvider, IConversationItemProvider conversationItemProvider)
        {
            Id = id;
            MessageProvider = messageProvider;
            ConversationItemProvider = conversationItemProvider;
        }
    }
}