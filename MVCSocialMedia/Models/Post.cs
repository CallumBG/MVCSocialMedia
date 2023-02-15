using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace MVCSocialMedia.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string OpinionText { get; set; }
        public byte[]? PostImageAsByteArray { get; set; }

        public Post()
        {

        }

        public Post(string title, string username, string opinionText, byte[] postImageAsByteArray)
        {
            Title = title;
            Username = username;
            OpinionText = opinionText;

            PostImageAsByteArray = postImageAsByteArray;
        }
    }


}
