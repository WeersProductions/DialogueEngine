using DialogueEngine.Conversation.RenderInfos;

namespace DialogueEngine.Conversation.ConversationItemProviders
{
    public class MultipleChoiceConversationItemProvider: IConversationItemProvider
    {
        /// <summary>
        /// Labels used on the buttons of multiple choice.
        /// </summary>
        private readonly string[] _labels;

        public MultipleChoiceConversationItemProvider(string[] labels)
        {
            _labels = labels;
        }

        public ConversationItemRenderInfo Render(ConversationStep[] steps)
        {
            return new MultipleChoiceConversationItemRenderInfo(_labels);
        }

        public string[] GetLabels()
        {
            return _labels;
        }
    }
}