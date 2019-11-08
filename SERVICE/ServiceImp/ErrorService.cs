using DATAACCESS.IRepository;
using DOMAIN.Models;
using Infrastructure.UnitOfWork;
using SERVICE.IService;

namespace SERVICE.ServiceImp
{
    public class ErrorService : BaseService, IErrorService
    {
        private readonly IErrorRepository _errorRepository;

        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._errorRepository = errorRepository;
        }

        public void CreateNew(Error model)
        {
            _errorRepository.CreateNew(model);
        }
    }
}