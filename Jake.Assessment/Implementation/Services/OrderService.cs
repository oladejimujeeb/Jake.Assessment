using Jake.Assessment.DTO;
using Jake.Assessment.Interface.IRepository;
using Jake.Assessment.Interface.IServices;
using Jake.Assessment.Model;
using Microsoft.EntityFrameworkCore;

namespace Jake.Assessment.Implementation.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }


        public async Task<BaseResponseModel> CreateOrder(CreateOrderRequest model)
        {
            if(_orderRepository.Get(model.UserId) ==  null) 
            {
                return new BaseResponseModel { Message = "User does not exist", Status = false };
            }
            var userSelectedProducts = await _productRepository.GetList(model.UserOrders.Select(x=>x.ProductId).ToList());

            if(userSelectedProducts.Count != model.UserOrders.Count)
            {
                return new BaseResponseModel { Status = false, Message = "One or more selected product not found" };
            }

            var userOders = new List<Order>(model.UserOrders.Count);

            foreach(var userOrder in model.UserOrders) 
            {
                var order = new Order 
                {
                    ProductId = userOrder.ProductId,
                     Quantity = userOrder.Quantity,
                     UserId = model.UserId,
                };

                userOders.Add(order);

                
            }
            try
            {
                await _orderRepository.AddRange(userOders);
                return new BaseResponseModel { Status = true, Message = "Order created successfully" };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel { Status = false, Message = $"Failed to save order; Error:{ex.Message}" };
            }
            
        }

        public async  Task<BaseResponseModel<IEnumerable<AllOrderViewModel>>> GetAllOrders()
        {
            var allOrders =  _orderRepository.Query().Include(o => o.Product).Include(o => o.User).AsNoTracking();
            if(!allOrders.Any()) 
            {
                return new BaseResponseModel<IEnumerable<AllOrderViewModel>>
                {
                    Status = true,
                    Data= Enumerable.Empty<AllOrderViewModel>(),
                    Message ="No Data"
                };
            }

            var data = await allOrders.Select(or => new AllOrderViewModel 
            {
                Email = or.User.Email,
                OrderId = or.Id,
                ProductName = or.Product.Name,
                ProductPrice = or.Product.Price,
                Quantity = or.Quantity,
                Username = or.User.Username
            }).ToListAsync();
            return new BaseResponseModel<IEnumerable<AllOrderViewModel>>
            {
                Status = true,
                Data = data,
                Message = "success"
            };
        }
    }
}
