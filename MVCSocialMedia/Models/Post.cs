using Microsoft.EntityFrameworkCore;

namespace MVCSocialMedia.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Username { get; set; } = "DefaultName";
        public string OpinionText { get; set; }
    }
}
