using DATAACCESS.IRepository;
using DOMAIN.Models;
using Infrastructure.DBFactory;
using Infrastructure.Repository;

namespace DATAACCESS.RepositoryImp
{
    public class ErrorRepository : RepositoryBase<Error, int>, IErrorRepository
    {
        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}