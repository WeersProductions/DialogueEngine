using System;
using System.IO;
using System.Threading.Tasks;
using DialogueEngine.Loading.JSONLoading;
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

        public async Task<LoadResult<LoadedConversations>> Load()
        {
            // TODO: open and read from file, convert to correct instances.
            // Should first initialize all messages, then conversationItems, then conversationSteps.
            LoadedConversations loadedConversations = null;

            using (StreamReader file = File.OpenText(_filePath))
            using (JsonReader reader = new JsonTextReader(file))
            {
                JsonSerializer serializer = new JsonSerializer();
                JSONContainer jsonContainer = serializer.Deserialize<JSONContainer>(reader);
            }

            return new LoadResult<LoadedConversations>(true, loadedConversations);
        }
    }
}