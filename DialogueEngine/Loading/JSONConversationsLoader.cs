using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DialogueEngine.Conversation;
using DialogueEngine.Conversation.ConversationItemProviders;
using DialogueEngine.Loading.JSONLoading;
using DialogueEngine.Loading.JSONLoading.ConversationData;
using DialogueEngine.Messages;
using DialogueEngine.Messages.Providers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DialogueEngine.Loading
{
    public class JSONConversationsLoader: IConversationsLoader
    {
        private readonly string _filePath;

        public JSONConversationsLoader(string filePath)
        {
            _filePath = filePath;
        }

        // TODO: unit tests!
        private ConversationItem CreateConversationItem(JSONConversationItem jsonConversationItem, Dictionary<int, Message> messages)
        {
            IMessageProvider messageProvider;
            switch (jsonConversationItem.MessageType)
            {
                case "random":
                {
                    var selectedMessages = new List<Message>();
                    foreach (var messageId in jsonConversationItem.MessageIds)
                    {
                        if (messages.TryGetValue(messageId, out var message))
                        {
                            selectedMessages.Add(message);
                        }
                    }

                    messageProvider = new RandomMessageProvider(selectedMessages.ToArray());
                    break;
                }
                case "default":
                {
                    Message message = null;
                    foreach (var messageId in jsonConversationItem.MessageIds)
                    {
                        if (messages.TryGetValue(messageId, out var tryMessage))
                        {
                            message = tryMessage;
                            break;
                        }
                    }

                    messageProvider = new SimpleMessageProvider(message);
                    break;
                }
                default:
                    messageProvider = null;
                    break;
            }

            IConversationItemProvider conversationItemProvider;
            switch (jsonConversationItem.ConversationType)
            {
                case "MultipleChoice":
                    var labels = (jsonConversationItem.ConversationData as JSONCDMultipleChoice)?.Labels;
                    conversationItemProvider = new MultipleChoiceConversationItemProvider(labels);
                    break;
                case "Simple":
                    conversationItemProvider = new SimpleConversationItemProvider();
                    break;
                default:
                    conversationItemProvider = null;
                    break;
            }
            
            return new ConversationItem(jsonConversationItem.Id, messageProvider, conversationItemProvider);
        }

        private LoadedConversations CreateLoadedConversations(JSONContainer jsonContainer)
        {
            Dictionary<int, Message> messages = new Dictionary<int, Message>(jsonContainer.Messages.Length);
            foreach (var jsonMessage in jsonContainer.Messages)
            {
                messages.Add(jsonMessage.Id, new Message(jsonMessage.RawString, jsonMessage.Id));
            }
            
            Dictionary<int, ConversationItem> conversationItems = new Dictionary<int, ConversationItem>(jsonContainer.ConversationItems.Length);
            foreach (var jsonConversationItem in jsonContainer.ConversationItems)
            {
                conversationItems.Add(jsonConversationItem.Id, CreateConversationItem(jsonConversationItem, messages));
            }
            
            Dictionary<int, ConversationStep> conversationSteps = new Dictionary<int, ConversationStep>(jsonContainer.ConversationSteps.Length);
            foreach (var jsonConversationStep in jsonContainer.ConversationSteps)
            {
                conversationSteps.Add(jsonConversationStep.Id, new ConversationStep(jsonConversationStep.Id, conversationItems[jsonConversationStep.ConversationItemId], new ConversationStep[jsonConversationStep.NextConversationStepIds.Length]));
            }

            // Update the links to other conversationSteps.
            foreach (var jsonConversationStep in jsonContainer.ConversationSteps)
            {
                int index = 0;
                foreach (var nextConversationStepId in jsonConversationStep.NextConversationStepIds)
                {
                    conversationSteps[jsonConversationStep.Id].NextStep[index] =
                        conversationSteps[nextConversationStepId];
                    index += 1;
                }
            }
            
            return new LoadedConversations(conversationSteps);
        }
        
        public async Task<LoadResult<LoadedConversations>> Load()
        {
            LoadedConversations loadedConversations;

            using (StreamReader file = File.OpenText(_filePath))
            using (JsonReader reader = new JsonTextReader(file))
            {
                JsonSerializer serializer = new JsonSerializer();
                JSONContainer jsonContainer = serializer.Deserialize<JSONContainer>(reader);
                loadedConversations = CreateLoadedConversations(jsonContainer);
            }

            return new LoadResult<LoadedConversations>(true, loadedConversations);
        }
    }
}