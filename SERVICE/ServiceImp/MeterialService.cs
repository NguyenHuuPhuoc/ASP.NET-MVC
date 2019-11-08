using DATAACCESS.IRepository;
using DOMAIN.Models;
using Infrastructure.UnitOfWork;
using SERVICE.IService;
using System.Collections.Generic;

namespace SERVICE.ServiceImp
{
    public class MeterialService : BaseService, IMeterialService
    {
        private readonly IMeterialRepository _meteRepository;

        public MeterialService(IUnitOfWork unitOfWork, IMeterialRepository meteRepository) : base(unitOfWork)
        {
            this._meteRepository = meteRepository;
        }

        public bool CheckContain(string name)
        {
            return _meteRepository.CheckContains(n => n.Name.Equals(name));
        }

        public bool CheckContain(int id, string name)
        {
            return _meteRepository.CheckContains(n => n.Name.Equals(name) && n.Id != id);
        }

        public void CreateNew(Meterial meterial)
        {
            _meteRepository.CreateNew(meterial);
        }

        public void Delete(int id)
        {
            _meteRepository.Delete(id);
        }

        public IList<Meterial> GetByFilter(string keyWord, int currentPage, int pageSize, out int totalRow)
        {
            return _meteRepository.GetByFilter(keyWord, currentPage, pageSize, out totalRow);
        }

        public Meterial GetByKey(int id)
        {
            return _meteRepository.GetSingleById(id);
        }

        public void Update(Meterial meterial)
        {
            _meteRepository.Update(meterial);
        }
    }
}