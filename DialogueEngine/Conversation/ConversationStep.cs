namespace DialogueEngine.Conversation
{
    public class ConversationStep
    {
        public readonly int Id;
        public readonly ConversationItem ConversationItem;
        public readonly ConversationStep[] NextStep;

        public ConversationStep(int id, ConversationItem conversationItem, ConversationStep[] nextStep)
        {
            Id = id;
            ConversationItem = conversationItem;
            NextStep = nextStep;
        }
    }
}