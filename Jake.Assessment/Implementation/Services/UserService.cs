using Jake.Assessment.DTO;
using Jake.Assessment.Implementation.Repository;
using Jake.Assessment.Interface.IRepository;
using Jake.Assessment.Interface.IServices;
using Jake.Assessment.Model;
using Microsoft.EntityFrameworkCore;

namespace Jake.Assessment.Implementation.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponseModel> CreateNewUser(CreateUserRequestModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                return new BaseResponseModel { Status = false, Message = "Email is not correct or not properly enter" };
            }
            var user = new User 
            { 
                Email = model.Email,
                Username = string.IsNullOrEmpty(model.Username) ? model.Email : model.Username,
            };
            try
            {
                await _userRepository.Add(user);
                return new BaseResponseModel { Status = true, Message = "User created successfully" };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel { Status = false, Message = $"Failed to save user; Error:{ex.Message}" };
            }
        }

        public async Task<BaseResponseModel<AllUserOrders>>AllUserOrder(int userId)
        {
            var allOrders =await _userRepository.Query().Include(x=>x.Orders).ThenInclude(x=>x.Product).Where(x=>x.Id == userId).FirstOrDefaultAsync();
            if (allOrders is null)
            {
                return new BaseResponseModel<AllUserOrders>
                {
                    Status = true,
                    Data = new AllUserOrders(),
                    Message = "No Order found"
                };
            }
           
            var data = new AllUserOrders
            {
                Email = allOrders.Email,
                UserId = allOrders.Id,
                Username = allOrders.Username,
                UserOrders = allOrders.Orders.Select(o => new UserOrders
                {
                    OrderId = o.Id,
                    ProductName = o.Product.Name,
                    ProductPrice = o.Product.Price,
                    Quantity = o.Quantity,

                }).ToList(),
            };

            return new BaseResponseModel<AllUserOrders>
            {
                Status = true,
                Data = data,
                Message = "No Order found"
            };
        }
    }
}
