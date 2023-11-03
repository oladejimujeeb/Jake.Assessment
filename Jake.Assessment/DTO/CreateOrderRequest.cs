using System.ComponentModel.DataAnnotations;

namespace Jake.Assessment.DTO
{
    public class CreateOrderRequest
    {
        [Required]
        public int UserId { get; set; }
        public IList<UserOrder> UserOrders { get; set; }= new List<UserOrder>();
    }
    public class UserOrder
    {
        [Required]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
