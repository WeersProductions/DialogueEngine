using System;

namespace DialogueEngine.Messages.Providers
{
    /// <summary>
    /// Returns a random message from a set.
    /// </summary>
    public class RandomMessageProvider: IMessageProvider
    {
        private readonly Message[] _messages;
        private Random _random;

        public RandomMessageProvider(Message[] messages)
        {
            _messages = messages;
        }

        public Message GetMessage()
        {
            if (_messages.Length <= 0)
            {
                return null;
            }

            if (_random == null)
            {
                _random = new Random();
            }
            return _messages[_random.Next(_messages.Length)];
        }
    }
}