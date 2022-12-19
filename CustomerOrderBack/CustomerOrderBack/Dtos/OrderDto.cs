namespace CustomerOrderBack.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderName { get; set; } = null!;
        public decimal Price { get; set; }
        public int CustomerId { get; set; }
    }
}
