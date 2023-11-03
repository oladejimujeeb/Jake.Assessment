using Jake.Assessment.DTO;

namespace Jake.Assessment.Interface.IServices
{
    public interface IProductService
    {
        Task<BaseResponseModel> CreateProduct(CreateProductRequestModel model);
        Task<BaseResponseModel> UpdateProduct(UpdateProductRequestModel model);
        Task<BaseResponseModel> DeleteProduct(int id);
        Task<BaseResponseModel<IEnumerable <AllProductViewModel>>> AllProduct();
    }
}
