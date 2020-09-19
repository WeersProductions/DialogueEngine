using System.Collections.Generic;
using DialogueEngine.Loading.JSONLoading.ConversationData;

namespace DialogueEngine.Loading.JSONLoading
{
    public class JSONConversationItem
    {
        public int Id { get; set; }
           /// <summary>
           /// E.g. random
           /// </summary>
        public string MessageType { get; set; }
        public int[] MessageIds { get; set; }
        /// <summary>
        /// E.g. multiple choice
        /// </summary>
        public string ConversationType { get; set; }
        public JSONConversationData ConversationData { get; set; }
    }
}