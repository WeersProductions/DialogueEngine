namespace DialogueEngine.Loading.JSONLoading.ConversationData
{
    public class JSONCDMultipleChoice: JSONConversationData
    {
        public string[] Labels { get; set; }
        public new string ConversationDataType { get; } = "MultipleChoice";
    }
}