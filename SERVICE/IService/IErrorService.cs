using DOMAIN.Models;

namespace SERVICE.IService
{
    public interface IErrorService : IBaseService
    {
        void CreateNew(Error model);
    }
}