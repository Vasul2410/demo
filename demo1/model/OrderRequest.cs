using System.ComponentModel.DataAnnotations;

namespace demo1.model
{
    public class OrderRequest
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public  List<OrderItemRequest> OrderItems { get; set;}
    }
}
