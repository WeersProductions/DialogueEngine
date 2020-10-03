using DialogueEngine.Conversation.ConversationItemProviders;

namespace DialogueEngine.Conversation.RenderInfos
{
    public class MultipleChoiceConversationItemRenderInfo: ConversationItemRenderInfo
    {
        public readonly string[] Labels; 
        public MultipleChoiceConversationItemRenderInfo(string[] labels) : base(ConversationItemProviderType.MultipleChoice)
        {
            Labels = labels;
        }
    }
}