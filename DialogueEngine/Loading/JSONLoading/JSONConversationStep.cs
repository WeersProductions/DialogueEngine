namespace DialogueEngine.Loading.JSONLoading
{
    public class JSONConversationStep
    {
        public int Id { get; set; }
        public int ConversationItemId { get; set; }
        public int[] NextConversationStepIds { get; set; }
    }
}