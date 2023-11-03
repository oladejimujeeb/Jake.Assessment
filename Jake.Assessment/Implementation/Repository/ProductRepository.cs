using Jake.Assessment.Context;
using Jake.Assessment.Interface.IRepository;
using Jake.Assessment.Model;

namespace Jake.Assessment.Implementation.Repository
{
    public class ProductRepository:BaseRepository<Product>, IProductRepository 
    {
        public ProductRepository(ApplicationContext context)
        {
            Context = context;
        }
    }
}
