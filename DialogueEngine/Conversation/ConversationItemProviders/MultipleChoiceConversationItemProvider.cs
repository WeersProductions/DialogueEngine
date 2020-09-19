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
            throw new System.NotImplementedException();
        }

        public string[] GetLabels()
        {
            return _labels;
        }
    }
}