using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoxikaBlogApp.Models
{
    public class Comments
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="User Name is required")]
        [MaxLength(100, ErrorMessage ="User name should not be more than 100 Characters")]
        public string UserName { get; set; }

        [DataType(DataType.Date)]
        public DateTime CommentDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage ="Content is Required")]
        public string Content { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
