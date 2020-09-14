using DialogueEngine.Conversation;
using DialogueEngine.Messages;

namespace DialogueEngine.ToExternal
{
    /// <summary>
    /// Contains all data that is required for a part of a conversation.
    /// </summary>
    public readonly struct ConversationElement
    {
        public readonly Message Message;
        public readonly ConversationItemRenderInfo ConversationItemRenderInfo;

        public ConversationElement(Message message, ConversationItemRenderInfo conversationItemRenderInfo)
        {
            Message = message;
            ConversationItemRenderInfo = conversationItemRenderInfo;
        }
    }
}