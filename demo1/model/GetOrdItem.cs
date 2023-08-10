using System.ComponentModel.DataAnnotations;

namespace demo1.model
{
    public class GetOrdItem
    {
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal price { get; set; }
        public int Quantity { get; set; }
        public decimal ItemTotal { get; set; }
    }
}
