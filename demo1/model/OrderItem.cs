using System.ComponentModel.DataAnnotations;

namespace demo1.model
{
    public class OrderItem
    {
        [Key]
        public int ItemId { get; set; }
        
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal ItemTotal { get; set; }

    }
}
