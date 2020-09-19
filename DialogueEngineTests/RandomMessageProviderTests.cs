using DialogueEngine.Messages;
using DialogueEngine.Messages.Providers;
using NUnit.Framework;

namespace DialogueEngineTests
{
    [TestFixture]
    public class RandomMessageProviderTests
    {
        [Test]
        public void GetMessage_NoMessages_Null()
        {
            var randomMessageProvider = new RandomMessageProvider(new Message[0]);
            Assert.IsNull(randomMessageProvider.GetMessage());
        }

        [Test]
        public void GetMessage_SingleMessage_Message()
        {
            var message = new Message("Hello!", 0);
            var randomMessageProvider = new RandomMessageProvider(new []{message});

            var receivedMessage = randomMessageProvider.GetMessage();
            Assert.IsNotNull(receivedMessage);
            Assert.AreEqual(message, receivedMessage);
        }
        
        [Test]
        public void GetMessage_MultipleMessages_OneOfThem()
        {
            var message = new Message("Hello!", 0);
            var message1 = new Message("Bye!", 1);
            var randomMessageProvider = new RandomMessageProvider(new []{message, message1});

            var receivedMessage = randomMessageProvider.GetMessage();
            Assert.IsNotNull(receivedMessage);
            CollectionAssert.Contains(new [] {message, message1}, receivedMessage);
        }
    }
}