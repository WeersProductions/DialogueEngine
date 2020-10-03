using DialogueEngine.Conversation;
using DialogueEngine.Conversation.ConversationItemProviders;
using DialogueEngine.Conversation.RenderInfos;
using NUnit.Framework;

namespace DialogueEngineTests
{
    [TestFixture]
    public class SimpleConversationItemProviderTests
    {
        [Test]
        public void Render()
        {
            var multipleChoiceConversationItemProvider = new SimpleConversationItemProvider();
            var conversationItemRenderInfo = multipleChoiceConversationItemProvider.Render(new ConversationStep[0]);
            
            Assert.NotNull(conversationItemRenderInfo);
            Assert.IsTrue(conversationItemRenderInfo is SimpleConversationItemRenderInfo);
        }
    }
}