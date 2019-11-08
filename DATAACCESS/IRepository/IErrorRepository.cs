using DOMAIN.Models;
using Infrastructure.Repository;

namespace DATAACCESS.IRepository
{
    public interface IErrorRepository : IRepositoryBase<Error, int>
    {
    }
}