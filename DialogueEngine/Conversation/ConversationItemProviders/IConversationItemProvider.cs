using DialogueEngine.Conversation.RenderInfos;

namespace DialogueEngine.Conversation.ConversationItemProviders
{
    public enum ConversationItemProviderType
    {
        Simple,
        MultipleChoice
    }
    
    public interface IConversationItemProvider
    {
        ConversationItemRenderInfo Render(ConversationStep[] steps);
    }
}