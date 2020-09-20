using System.Collections.Generic;
using DialogueEngine.Conversation;
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

        [Test]
        public void CreateLoadedConversations_Empty_Empty()
        {
            var jsonContainer = new JSONContainer {ConversationItems = new JSONConversationItem[0], Messages = new JSONMessage[0], ConversationSteps = new JSONConversationStep[0]};
            var loadedConversations = JSONConversationsLoader.CreateLoadedConversations(jsonContainer);
            
            Assert.NotNull(loadedConversations);
            Assert.IsEmpty(loadedConversations.Conversations);
        }

        [Test]
        public void CreateLoadedConversations_SingleStep_SingleEntry()
        {
            var expectedOutput = new ConversationStep(
                0, 
                new ConversationItem(
                    0, 
                    new SimpleMessageProvider( new Message("hi!", 0)), 
                    new SimpleConversationItemProvider() ),
                new ConversationStep[0]);
            
            var input = new JSONContainer
            {
                Messages = new []
                {
                    new JSONMessage {Id = 0, RawString = "hi!"},
                },
                ConversationItems = new []
                {
                    new JSONConversationItem
                    {
                        ConversationType = "simple", 
                        Id = 0, 
                        MessageType = "simple", 
                        MessageIds = new []{0}
                    }, 
                },
                ConversationSteps = new []
                {
                    new JSONConversationStep
                    {
                        Id = 0,
                        ConversationItemId = 0,
                        NextConversationStepIds = new int[0]
                    }, 
                }
            };


            var loadedConversations = JSONConversationsLoader.CreateLoadedConversations(input);
            Assert.NotNull(loadedConversations);
            Assert.AreEqual(1, loadedConversations.Conversations.Count);
            var actualOutput = loadedConversations.Conversations[0];
            
            // Compare output.
            Assert.AreEqual(expectedOutput.Id, actualOutput.Id, "ConversationStep Id should be equal.");
            CollectionAssert.AreEqual(expectedOutput.NextStep, actualOutput.NextStep, "Next steps should be equal.");
            Assert.AreEqual(expectedOutput.ConversationItem, actualOutput.ConversationItem, "Conversation item should be equal.");
        }

        [Test]
        public void CreateLoadedConversations_MultiStep_MultipleEntries()
        {
            var messages = new[]
            {
                new Message("hi!", 0), 
                new Message("bye...", 1)
            };
            var expectedOutput0 = new ConversationStep(
                0, 
                new ConversationItem(
                    0, 
                    new SimpleMessageProvider(messages[0]), 
                    new SimpleConversationItemProvider()),
                new ConversationStep[0]);
            var expectedOutput1 = new ConversationStep(
                1, 
                new ConversationItem(
                    1, 
                    new RandomMessageProvider(messages),
                    new SimpleConversationItemProvider()), 
                new []{expectedOutput0});
            
            var input = new JSONContainer
            {
                Messages = new []
                {
                    new JSONMessage {Id = 0, RawString = "hi!"},
                    new JSONMessage {Id = 1, RawString = "bye..."}, 
                },
                ConversationItems = new []
                {
                    new JSONConversationItem
                    {
                        ConversationType = "simple", 
                        Id = 0, 
                        MessageType = "simple", 
                        MessageIds = new []{0}
                    }, 
                    new JSONConversationItem
                    {
                        ConversationType = "simple",
                        Id = 1,
                        MessageType = "random",
                        MessageIds = new []{0, 1}
                    }
                },
                ConversationSteps = new []
                {
                    new JSONConversationStep
                    {
                        Id = 0,
                        ConversationItemId = 0,
                        NextConversationStepIds = new int[0]
                    }, 
                    new JSONConversationStep
                    {
                        Id = 1,
                        ConversationItemId = 1,
                        NextConversationStepIds = new []{0}
                    }
                }
            };


            var loadedConversations = JSONConversationsLoader.CreateLoadedConversations(input);
            Assert.NotNull(loadedConversations);
            Assert.AreEqual(2, loadedConversations.Conversations.Count);
            
            // Compare output 0.
            var actualOutput0 = loadedConversations.Conversations[0];
            Assert.AreEqual(expectedOutput0.Id, actualOutput0.Id, "Output0: ConversationStep Id should be equal.");
            CollectionAssert.AreEqual(expectedOutput0.NextStep, actualOutput0.NextStep, "Output0: Next steps should be equal.");
            Assert.AreEqual(expectedOutput0.ConversationItem, actualOutput0.ConversationItem, "Output0: Conversation item should be equal.");
            
            // Compare output 1.
            var actualOutput1 = loadedConversations.Conversations[1];
            Assert.AreEqual(expectedOutput1.Id, actualOutput1.Id, "Output1: ConversationStep Id should be equal.");
            CollectionAssert.AreEqual(new []{actualOutput0}, actualOutput1.NextStep, "Output1: Next steps should be equal.");
            Assert.AreEqual(expectedOutput1.ConversationItem, actualOutput1.ConversationItem, "Output1: Conversation item should be equal.");
        }
    }
}