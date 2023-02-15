using System.ComponentModel.DataAnnotations;

namespace MVCSocialMedia.Services
{
    public class CreatePostRequest
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string? Username { get; set; }
        [Required]
        public string OpinionText { get; set; }
        public IFormFile? File { get; set; }

        public CreatePostRequest(string title, string username, string text)
        {
            this.Title = title;
            this.Username = username;
            this.OpinionText = text;
        }

        public CreatePostRequest()
        {

        }
    }
}
