namespace Jake.Assessment.DTO
{
    public record AllProductViewModel
    {
        public int ProductId { get; set; }
        public string Productname { get; set; }
        public decimal ProductPrice { get; set; }
    };
   
}
