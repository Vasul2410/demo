using System.ComponentModel.DataAnnotations;

namespace demo1.model
{
    public class OrderItemRequest
    {
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
