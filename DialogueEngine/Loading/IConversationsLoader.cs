using System.Threading.Tasks;

namespace DialogueEngine.Loading
{
    public interface IConversationsLoader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loadedConversations"></param>
        /// <returns>True if successful.</returns>
        bool Load(out LoadedConversations loadedConversations);
    }
}