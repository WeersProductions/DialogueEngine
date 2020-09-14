namespace DialogueEngine.Conversation.ConversationItemProviders
{
    public interface IConversationItemProvider
    {
        ConversationItemRenderInfo Render(ConversationStep[] steps);
    }
}