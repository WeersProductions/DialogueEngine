using DialogueEngine.Conversation.RenderInfos;

namespace DialogueEngine.Conversation.ConversationItemProviders
{
    public class SimpleConversationItemProvider: IConversationItemProvider
    {
        public ConversationItemRenderInfo Render(ConversationStep[] steps)
        {
            return new SimpleConversationItemRenderInfo();
        }

        protected bool Equals(SimpleConversationItemProvider other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SimpleConversationItemProvider) obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}