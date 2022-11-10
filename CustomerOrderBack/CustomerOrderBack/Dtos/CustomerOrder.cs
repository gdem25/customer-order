using CustomerOrderModel.Models;

namespace CustomerOrderBack.Dtos
{
    public class CustomerOrder
    {
        public CustomerOrder()
        {
            Orders = new List<Order>();
        }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public ICollection<Order> Orders { get; set; }

    }
}
