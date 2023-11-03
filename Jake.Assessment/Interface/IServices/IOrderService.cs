using Jake.Assessment.DTO;

namespace Jake.Assessment.Interface.IServices
{
    public interface IOrderService
    {
        Task<BaseResponseModel> CreateOrder(CreateOrderRequest model);
        Task<BaseResponseModel<IEnumerable<AllOrderViewModel>>>GetAllOrders();
    }
}
