namespace Jake.Assessment.Model
{
    public class User:BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
