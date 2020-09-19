using DialogueEngine.Messages;
using DialogueEngine.Messages.Providers;
using NUnit.Framework;

namespace DialogueEngineTests
{
    [TestFixture]
    public class SimpleMessageProviderTests
    {
        [Test]
        public void GetMessage_NoMessages_Null()
        {
            var simpleMessageProvider = new SimpleMessageProvider(null);
            Assert.IsNull(simpleMessageProvider.GetMessage());
        }

        [Test]
        public void GetMessage_GiveMessage_GivenMessage()
        {
            var message = new Message("Hi!", 1);
            var simpleMessageProvider = new SimpleMessageProvider(message);

            var receivedMessage = simpleMessageProvider.GetMessage();
            Assert.IsNotNull(receivedMessage);
            Assert.AreEqual(message, receivedMessage);
        }
    }
}