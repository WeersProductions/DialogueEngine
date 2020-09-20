using System;
using System.Linq;

namespace DialogueEngine.Messages.Providers
{
    /// <summary>
    /// Returns a random message from a set.
    /// </summary>
    public class RandomMessageProvider: IMessageProvider
    {
        private readonly Message[] _messages;
        private readonly Random _random;

        public RandomMessageProvider(Message[] messages)
        {
            _messages = messages;
            _random = new Random();
        }

        public Message GetMessage()
        {
            if (_messages.Length <= 0)
            {
                return null;
            }
            return _messages[_random.Next(_messages.Length)];
        }

        protected bool Equals(RandomMessageProvider other)
        {
            return _messages.SequenceEqual(other._messages);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RandomMessageProvider) obj);
        }

        public override int GetHashCode()
        {
            return (_messages != null ? _messages.GetHashCode() : 0);
        }
    }
}