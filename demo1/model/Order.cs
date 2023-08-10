using System.ComponentModel.DataAnnotations;

namespace demo1.model
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string Status { get; set; }

        public decimal OrderTotal { get; set; }

    }
}
