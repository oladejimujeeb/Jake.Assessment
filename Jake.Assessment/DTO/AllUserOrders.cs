namespace Jake.Assessment.DTO
{
    public class AllUserOrders
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserOrders> UserOrders {  get; set; }= new List<UserOrders>();
    }
    public class UserOrders
    {
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
