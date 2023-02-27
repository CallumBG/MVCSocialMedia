using MVCSocialMedia.Models;

namespace MVCSocialMedia.Data
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetPosts();
        Task<Post> GetByIdAsync(int? id);
        Task InsertPost(Post post);
        Task DeletePost(Post post);
        Task UpdatePost(Post post);

        bool DoesPostExist(int id);

        IEnumerable<Post> ViewSpecificUserPosts(string username);
        IEnumerable<Post> GetSearchResults(string SearchPhrase);
    }
}
