using System.Collections.Generic;
using DialogueEngine.Conversation.ConversationItemProviders;
using DialogueEngine.Loading;
using DialogueEngine.Loading.JSONLoading;
using DialogueEngine.Loading.JSONLoading.ConversationData;
using DialogueEngine.Messages;
using DialogueEngine.Messages.Providers;
using NUnit.Framework;

namespace DialogueEngineTests
{
    [TestFixture]
    public class JSONConversationsLoaderTests
    {
        [Test]
        public void CreateConversationItem_RandomMessageType_RandomMessageProvider()
        {
            var jsonConversationItem = new JSONConversationItem {MessageType = "random", MessageIds = new int[0]};
            var conversationItem =
                JSONConversationsLoader.CreateConversationItem(jsonConversationItem, new Dictionary<int, Message>());
            
            Assert.NotNull(conversationItem);
            Assert.IsInstanceOf<RandomMessageProvider>(conversationItem.MessageProvider);
        }

        [Test]
        public void CreateConversationItem_SimpleMessageType_SimpleMessageProvider()
        {
            var jsonConversationItem = new JSONConversationItem {MessageType = "simple", MessageIds = new int[0]};
            var conversationItem =
                JSONConversationsLoader.CreateConversationItem(jsonConversationItem, new Dictionary<int, Message>());
            
            Assert.NotNull(conversationItem);
            Assert.IsInstanceOf<SimpleMessageProvider>(conversationItem.MessageProvider);
        }
        
        [Test]
        public void CreateConversationItem_SimpleMessageTypeMessage_SimpleMessageProviderMessage()
        {
            var messages = new Dictionary<int, Message>() {{1, new Message("hey :)", 1)}};
            var jsonConversationItem = new JSONConversationItem {MessageType = "simple", MessageIds = new []{1}};
            var conversationItem =
                JSONConversationsLoader.CreateConversationItem(jsonConversationItem, messages);
            
            Assert.NotNull(conversationItem);
            Assert.IsInstanceOf<SimpleMessageProvider>(conversationItem.MessageProvider);
            Assert.AreEqual(messages[1], conversationItem.MessageProvider.GetMessage());
        }

        [Test]
        public void CreateConversationItem_UndefinedMessageType_NullMessageProvider()
        {
            var jsonConversationItem = new JSONConversationItem {MessageType = "undefined", MessageIds = new int[0]};
            var conversationItem =
                JSONConversationsLoader.CreateConversationItem(jsonConversationItem, new Dictionary<int, Message>());
            
            Assert.NotNull(conversationItem);
            Assert.Null(conversationItem.MessageProvider);
        }

        [Test]
        public void CreateConversationItem_MultipleChoiceConversationType_MultipleChoiceConversationItemProvider()
        {
            var jsonConversationItem = new JSONConversationItem {ConversationType = "multipleChoice"};
            var conversationItem =
                JSONConversationsLoader.CreateConversationItem(jsonConversationItem, new Dictionary<int, Message>());
            
            Assert.NotNull(conversationItem);
            Assert.IsInstanceOf<MultipleChoiceConversationItemProvider>(conversationItem.ConversationItemProvider);
        }

        [Test]
        public void CreateConversationItem_MultipleChoiceConversationTypeLabels_MultipleChoiceLabels()
        {
            var labels = new [] {"label1", "label2"};
            var jsonConversationItem = new JSONConversationItem
            {
                ConversationType = "multipleChoice", 
                ConversationData = new JSONCDMultipleChoice() {Labels = labels}
            };
            var conversationItem =
                JSONConversationsLoader.CreateConversationItem(jsonConversationItem, new Dictionary<int, Message>());
            
            Assert.NotNull(conversationItem);
            Assert.IsInstanceOf<MultipleChoiceConversationItemProvider>(conversationItem.ConversationItemProvider);
            Assert.AreEqual(labels, (conversationItem.ConversationItemProvider as MultipleChoiceConversationItemProvider)?.GetLabels());
        }

        [Test]
        public void CreateConversationItem_SimpleConversationType_SimpleConversationItemProvider()
        {
            var jsonConversationItem = new JSONConversationItem {ConversationType = "simple"};
            var conversationItem =
                JSONConversationsLoader.CreateConversationItem(jsonConversationItem, new Dictionary<int, Message>());
            
            Assert.NotNull(conversationItem);
            Assert.IsInstanceOf<SimpleConversationItemProvider>(conversationItem.ConversationItemProvider);
        }

        [Test]
        public void CreateConversationItem_UndefinedConversationType_NullConversationItemProvider()
        {
            var jsonConversationItem = new JSONConversationItem {ConversationType = "undefined"};
            var conversationItem =
                JSONConversationsLoader.CreateConversationItem(jsonConversationItem, new Dictionary<int, Message>());
            
            Assert.NotNull(conversationItem);
            Assert.Null(conversationItem.ConversationItemProvider);
        }
    }
}