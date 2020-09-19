using System.Threading.Tasks;

namespace DialogueEngine.Loading
{
    public struct LoadResult<T>
    {
        public bool Success;
        public T Result;

        public LoadResult(bool success, T result)
        {
            Success = success;
            Result = result;
        }
    }
    
    public interface IConversationsLoader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loadedConversations"></param>
        /// <returns>True if successful.</returns>
        Task<LoadResult<LoadedConversations>> Load();
    }
}