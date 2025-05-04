using System.ComponentModel.DataAnnotations;

namespace MoxikaBlogApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Category name is reuired ")]
        [MaxLength(100, ErrorMessage ="Category should not be more than 100 characters")]
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Post> Posts {  get; set; }

    }
}
