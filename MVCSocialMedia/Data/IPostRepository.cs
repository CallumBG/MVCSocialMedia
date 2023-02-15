using MVCSocialMedia.Models;

namespace MVCSocialMedia.Data
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPosts();
        Task<Post> GetByIdAsync(int? id);
        void InsertPost(Post post);
        void DeletePost(Post post);
        void UpdatePost(Post post);

        bool DoesPostExist(int id);

        IEnumerable<Post> ViewSpecificUserPosts(string username);
        IEnumerable<Post> GetSearchResults(string SearchPhrase);
    }
}
