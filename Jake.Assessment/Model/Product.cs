namespace Jake.Assessment.Model
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
