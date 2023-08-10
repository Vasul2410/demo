namespace demo1.model
{
    public class GetOrders
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; }
        public decimal OrderTotal { get; set; }
        public List<GetOrdItem> OrdItem { get; set; }
    }
}
