using Jake.Assessment.Context;
using Jake.Assessment.Interface.IRepository;
using Jake.Assessment.Model;

namespace Jake.Assessment.Implementation.Repository
{
    public class UserRepository:BaseRepository<User>, IUserRepository 
    {
        public UserRepository(ApplicationContext context)
        {
            Context = context;
        }
    }
}
