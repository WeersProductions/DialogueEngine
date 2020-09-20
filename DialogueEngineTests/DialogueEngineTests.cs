using System.Collections.Generic;
using System.Threading.Tasks;
using DialogueEngine.Conversation;
using DialogueEngine.Conversation.ConversationItemProviders;
using DialogueEngine.Loading;
using DialogueEngine.Messages;
using DialogueEngine.Messages.Providers;
using NUnit.Framework;

namespace DialogueEngineTests
{
    class MockupConversationLoader : IConversationsLoader
    {
        private readonly bool _success;
        private readonly LoadedConversations _loadedConversations;
        
        public MockupConversationLoader(bool success = true, LoadedConversations loadedConversations = null)
        {
            _success = success;
            _loadedConversations = loadedConversations;
        }
        
        public async Task<LoadResult<LoadedConversations>> Load()
        {
            return new LoadResult<LoadedConversations>(_success, _loadedConversations);
        }
    }
    
    [TestFixture]
    public class DialogueEngineTests
    {
        [Test]
        public void LoadImmediate_Success_Loaded()
        {
            var dialogueEngine = new DialogueEngine.DialogueEngine(new MockupConversationLoader(), true);
            
            Assert.IsTrue(dialogueEngine.Loaded);
        }

        [Test]
        public void LoadLazy_NotLoaded()
        {
            var dialogueEngine = new DialogueEngine.DialogueEngine(new MockupConversationLoader(), false);
            
            Assert.IsFalse(dialogueEngine.Loaded);
        }

        [Test]
        public void Load_Success_Loaded()
        {
            var dialogueEngine = new DialogueEngine.DialogueEngine(new MockupConversationLoader(), false);

            Assert.IsTrue(dialogueEngine.Load());
            Assert.IsTrue(dialogueEngine.Loaded);
        }
        
        [Test]
        public void Load_Fail_Loaded()
        {
            var dialogueEngine = new DialogueEngine.DialogueEngine(new MockupConversationLoader(false), false);

            Assert.IsFalse(dialogueEngine.Load());
            Assert.IsTrue(dialogueEngine.Loaded);
        }

        /// <summary>
        /// Getting an item should make sure everything is loaded.
        /// </summary>
        [Test]
        public void GetConversationElement_NotLoaded_Loaded()
        {
            var dialogueEngine = new DialogueEngine.DialogueEngine(new MockupConversationLoader(), false);
            dialogueEngine.GetConversationElement(-1);
            
            Assert.IsTrue(dialogueEngine.Loaded);
        }

        [Test]
        public void GetConversationElement_ExistingStep_NotNull()
        {
            var message = new Message("Hello!", 0);
            var conversationStep = new ConversationStep(
                1,
                new ConversationItem(
                    0, 
                    new SimpleMessageProvider(message), 
                    new SimpleConversationItemProvider()), 
                new ConversationStep[0]);
            var loadedConversations = new LoadedConversations(new Dictionary<int, ConversationStep> {{1, conversationStep}});
            var dialogueEngine = new DialogueEngine.DialogueEngine(new MockupConversationLoader(true, loadedConversations), true);

            var conversationElement = dialogueEngine.GetConversationElement(1);
            Assert.NotNull(conversationElement);
            Assert.AreEqual(message, conversationElement.Message);
        }
        
        [Test]
        public void GetConversationElement_UnknownStep_Null()
        {
            var message = new Message("Hello!", 0);
            var conversationStep = new ConversationStep(
                1,
                new ConversationItem(
                    0, 
                    new SimpleMessageProvider(message), 
                    new SimpleConversationItemProvider()), 
                new ConversationStep[0]);
            var loadedConversations = new LoadedConversations(new Dictionary<int, ConversationStep> {{1, conversationStep}});
            var dialogueEngine = new DialogueEngine.DialogueEngine(new MockupConversationLoader(true, loadedConversations), true);

            var conversationElement = dialogueEngine.GetConversationElement(0);
            Assert.NotNull(conversationElement);
            Assert.Null(conversationElement.Message);
        }
    }
}