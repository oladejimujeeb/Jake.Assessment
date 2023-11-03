using Jake.Assessment.Context;
using Jake.Assessment.Interface.IRepository;
using Jake.Assessment.Model;

namespace Jake.Assessment.Implementation.Repository
{
    public class OrderRepository:BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext context)
        {

            Context = context;
        }
    }
}
