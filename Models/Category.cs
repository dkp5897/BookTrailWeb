using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MilkyWeb.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(25)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(0, 100, ErrorMessage = "Display Order must be between 0 to 100")]
        public int DisplayOrder { get; set; }
    }
}
