using System.ComponentModel.DataAnnotations;

namespace Jake.Assessment.DTO
{
    public record CreateProductRequestModel([Required]string ProductName, decimal Price);
    public record class UpdateProductRequestModel([Required]int id,string? ProductName, decimal? Price);
}
