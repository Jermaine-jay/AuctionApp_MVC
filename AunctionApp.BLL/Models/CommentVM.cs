using System.ComponentModel.DataAnnotations;

namespace AunctionApp.BLL.Models
{
    public class CommentVM
    {
        public Guid? Id { get; set; }

        [Required, MaxLength(50)]
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Subject { get; set; }
        [MaxLength(500), Required(ErrorMessage = "Comment should not be more than 500 words.")]
        public string? Message { get; set; }
    }
}
