namespace Jake.Assessment.Model
{
    public class Order:BaseEntity
    {
        public int UserId {  get; set; }
        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
