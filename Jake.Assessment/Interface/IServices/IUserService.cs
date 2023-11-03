using Jake.Assessment.DTO;

namespace Jake.Assessment.Interface.IServices
{
    public interface IUserService
    {
        Task<BaseResponseModel> CreateNewUser(CreateUserRequestModel model);
        Task<BaseResponseModel<AllUserOrders>> AllUserOrder(int userId);
    }
}
