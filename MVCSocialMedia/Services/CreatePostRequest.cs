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
    }
}
