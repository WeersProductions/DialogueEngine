using JsonSubTypes;
using Newtonsoft.Json;

namespace DialogueEngine.Loading.JSONLoading.ConversationData
{
    [JsonConverter(typeof(JsonSubtypes), "ConversationDataType")]
    public class JSONConversationData: IConversationDataType
    {
        /// <summary>
        /// Test.
        /// </summary>
        public float Color { get; set; }
        public string ConversationDataType { get; } = "Default";
    }
}