using DialogueEngine.Conversation.ConversationItemProviders;

namespace DialogueEngine.Conversation.RenderInfos
{
    public class SimpleConversationItemRenderInfo: ConversationItemRenderInfo
    {
        public SimpleConversationItemRenderInfo() : base(ConversationItemProviderType.Simple)
        {
        }
    }
}