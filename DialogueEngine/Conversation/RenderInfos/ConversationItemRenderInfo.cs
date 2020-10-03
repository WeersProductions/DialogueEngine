using DialogueEngine.Conversation.ConversationItemProviders;

namespace DialogueEngine.Conversation.RenderInfos
{
    public class ConversationItemRenderInfo
    {
        /// <summary>
        /// Type of conversation. Used to determine what kind of subclass is used.
        /// </summary>
        public readonly ConversationItemProviderType ConversationType;

        public ConversationItemRenderInfo(ConversationItemProviderType conversationType)
        {
            ConversationType = conversationType;
        }
    }
}