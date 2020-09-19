namespace DialogueEngine.Loading.JSONLoading
{
    public class JSONContainer
    {
        public JSONMessage[] Messages { get; set; }
        public JSONConversationStep[] ConversationSteps { get; set; }
        public JSONConversationItem[] ConversationItems { get; set; }
    }
}