using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demo1.model
{
    public class Result
    {
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal price { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

    }
}
