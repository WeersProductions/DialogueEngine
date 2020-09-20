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

        protected bool Equals(ConversationItem other)
        {
            return Id == other.Id && Equals(MessageProvider, other.MessageProvider) && Equals(ConversationItemProvider, other.ConversationItemProvider);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ConversationItem) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (MessageProvider != null ? MessageProvider.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ConversationItemProvider != null ? ConversationItemProvider.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}