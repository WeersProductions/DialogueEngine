using DialogueEngine.Conversation;
using DialogueEngine.Conversation.ConversationItemProviders;
using DialogueEngine.Conversation.RenderInfos;
using NUnit.Framework;

namespace DialogueEngineTests
{
    [TestFixture]
    public class MultipleChoiceConversationItemProviderTests
    {
        [Test]
        public void Render_NoLabels_NoLabels()
        {
            var multipleChoiceConversationItemProvider = new MultipleChoiceConversationItemProvider(new string[0]);
            var conversationItemRenderInfo = multipleChoiceConversationItemProvider.Render(new ConversationStep[0]);
            
            Assert.NotNull(conversationItemRenderInfo);
            Assert.IsTrue(conversationItemRenderInfo is MultipleChoiceConversationItemRenderInfo);
            var multipleChoiceConversationItemRenderInfo = conversationItemRenderInfo as MultipleChoiceConversationItemRenderInfo;
            CollectionAssert.IsEmpty(multipleChoiceConversationItemRenderInfo.Labels);
        }
        
        [Test]
        public void Render_Labels_Labels()
        {
            var expectedLabels = new[] {"label1", "label2"};
            var multipleChoiceConversationItemProvider = new MultipleChoiceConversationItemProvider(expectedLabels);
            var conversationItemRenderInfo = multipleChoiceConversationItemProvider.Render(new ConversationStep[0]);
            
            Assert.NotNull(conversationItemRenderInfo);
            Assert.IsTrue(conversationItemRenderInfo is MultipleChoiceConversationItemRenderInfo);
            var multipleChoiceConversationItemRenderInfo = conversationItemRenderInfo as MultipleChoiceConversationItemRenderInfo;
            CollectionAssert.AreEqual(expectedLabels,multipleChoiceConversationItemRenderInfo.Labels);
        }
    }
}