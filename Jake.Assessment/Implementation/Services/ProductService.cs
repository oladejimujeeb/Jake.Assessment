using Jake.Assessment.DTO;
using Jake.Assessment.Interface.IRepository;
using Jake.Assessment.Interface.IServices;
using Jake.Assessment.Model;

namespace Jake.Assessment.Implementation.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<BaseResponseModel<IEnumerable<AllProductViewModel>>> AllProduct()
        {
            var products = _productRepository.Query().Select(x => new AllProductViewModel() 
            {
                ProductPrice = x.Price,
                Productname = x.Name,
                ProductId = x.Id
            });
            if (!products.Any())
            {
                return new BaseResponseModel<IEnumerable<AllProductViewModel>>()
                {
                    Data = Enumerable.Empty<AllProductViewModel>(),
                    Message = "No Data",
                    Status = true
                };
            }
            
            return new BaseResponseModel<IEnumerable<AllProductViewModel>>()
            {
                Data = products,
                Message = "Success",
                Status = true
            };
        }
    

        public async Task<BaseResponseModel> CreateProduct(CreateProductRequestModel model)
        {
            if(string.IsNullOrEmpty(model.ProductName))
            {
                return new BaseResponseModel { Message= "Product name cannot be null", Status = false};
            }
            if (model.Price < 1)
            {
                return new BaseResponseModel { Status = false, Message = "Product price cannot be less than 1" };
            }
            /*Random random = new Random();*/
            Product product = new()
            {
                Name = model.ProductName,
                Price = model.Price,
                //Id= random.Next(1, 10000)
            };
            try
            {
                var prod = await _productRepository.Add(product);
                if (prod is null) 
                {
                    return new BaseResponseModel { Status = false, Message = "Failed to save product" };
                }
                return new BaseResponseModel { Status = true, Message = "Product created successfully" };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel { Status = false, Message = $"Failed to save product; Error:{ex.Message}" };
            }
           
        }

        public async Task<BaseResponseModel> DeleteProduct(int id)
        {
            var product = await _productRepository.Get(id);
            if (product is null)
                return new BaseResponseModel
                        {
                            Status = false,
                            Message = "Product does not exist"
                        };
            try
            {
                var Isdeleted = await _productRepository.Delete(product);
                if (!Isdeleted)
                {
                    return new BaseResponseModel
                    {
                        Status = false,
                        Message = "Product failed to delete"
                    };
                }
                return new BaseResponseModel
                {
                    Status = true,
                    Message = "Product deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    Status = false,
                    Message = $"Product failed to delete; Error : {ex.Message}"
                };
            }
           
           
        }

        public async Task<BaseResponseModel> UpdateProduct(UpdateProductRequestModel model)
        {
            var product = await _productRepository.Get(model.id);
            if (product is null)
                return new BaseResponseModel
                {
                    Status = false,
                    Message = "Product does not exist"
                };

            if (string.IsNullOrEmpty(model.ProductName))
            {
                return new BaseResponseModel { Message = "Product name cannot be null", Status = false };
            }
            if (model.Price < 1)
            {
                return new BaseResponseModel { Status = false, Message = "Product price cannot be less than 1" };
            }
            product.Price = model.Price==null? product.Price: model.Price.Value;
            product.Name = string.IsNullOrEmpty(model.ProductName)? product.Name: model.ProductName;

            try
            {
                var updateProduct = await _productRepository.Update(product);
                if (updateProduct is null)
                {
                    return new BaseResponseModel { Status = false, Message = "Failed to update product" };
                }
                return new BaseResponseModel { Status = true, Message = "Product updated successfully" };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel { Status = false, Message = $"Failed to update product; Error:{ex.Message}" };
            }
        }

     
    }
}
