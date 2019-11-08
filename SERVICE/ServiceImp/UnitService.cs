using DATAACCESS.IRepository;
using DOMAIN.Models;
using Infrastructure.UnitOfWork;
using SERVICE.IService;
using System.Collections.Generic;

namespace SERVICE.ServiceImp
{
    public class UnitService : BaseService, IUnitService
    {
        private readonly IUnitRepository _unitRepository;

        public UnitService(IUnitRepository unitRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._unitRepository = unitRepository;
        }

        public bool CheckContain(string name)
        {
            return _unitRepository.CheckContains(n => n.Name.Equals(name));
        }

        public bool CheckContain(int id, string name)
        {
            return _unitRepository.CheckContains(n => n.Id != id && n.Name.Equals(name));
        }

        public void CreateNew(Unit unit)
        {
            _unitRepository.CreateNew(unit);
        }

        public void Delete(int Id)
        {
            _unitRepository.Delete(Id);
        }

        public IList<Unit> GetByFilter(string keyWord, int currentPage, int pageSize, out int totalRow)
        {
            return _unitRepository.GetByFilter(keyWord, currentPage, pageSize, out totalRow);
        }

        public Unit GetByKey(int Id)
        {
            return _unitRepository.GetSingleById(Id);
        }

        public void Update(Unit unit)
        {
            _unitRepository.Update(unit);
        }
    }
}