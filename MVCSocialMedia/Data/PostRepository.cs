using Microsoft.EntityFrameworkCore;
using MVCSocialMedia.Models;
using System.Web;

namespace MVCSocialMedia.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Show all posts

        public IEnumerable<Post> GetPosts()
        {
            return _context.Posts.OrderByDescending(x => x.Id).ToList();
        }

        //Show one post
        public async Task<Post> GetPostByID(int? id)
        {
            //return _context.Posts.Find(postID.ToString());
            return await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);
        }

        //AddPost
        public async void InsertPost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }


        //ShowUserPosts
        public IEnumerable<Post> ViewSpecificUserPosts(string username)
        {
            return _context.Posts.Where(x => x.Username.Equals(username)).ToList();
        }

        //ShowSearchResults
        public IEnumerable<Post> GetSearchResults(string SearchPhrase)
        {
            return _context.Posts.Where(j => j.Title.Contains(SearchPhrase)).ToList();
        }


        public async void DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        //EditPost
        public async void UpdatePost(Post post)
        {
            _context.Update(post);
            await _context.SaveChangesAsync();
        }

        //PostExists

        public bool DoesPostExist(int id)
        {
            return (_context.Posts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
