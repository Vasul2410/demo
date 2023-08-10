using System.ComponentModel.DataAnnotations;

namespace demo1.model
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

    }
}
