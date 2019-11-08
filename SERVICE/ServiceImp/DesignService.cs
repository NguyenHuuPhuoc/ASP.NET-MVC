using DATAACCESS.IRepository;
using DOMAIN.Models;
using Infrastructure.UnitOfWork;
using SERVICE.IService;
using System.Collections.Generic;

namespace SERVICE.ServiceImp
{
    public class DesignService : BaseService, IDesignService
    {
        private readonly IDesignRepository _designRepository;

        public DesignService(IUnitOfWork unitOfWork, IDesignRepository designRepository) : base(unitOfWork)
        {
            this._designRepository = designRepository;
        }

        public bool CheckContain(string name)
        {
            return _designRepository.CheckContains(n => n.Name.Equals(name));
        }

        public bool CheckContain(int id, string name)
        {
            return _designRepository.CheckContains(n => n.Name.Equals(name) && n.Id != id);
        }

        public void CreateNew(Design design)
        {
            _designRepository.CreateNew(design);
        }

        public void Delete(int id)
        {
            _designRepository.Delete(id);
        }

        public IList<Design> GetByFilter(string keyWord, int? categoryParentId, int currentPage, int pageSize, out int totalRow)
        {
            return _designRepository.GetByFilter(keyWord, categoryParentId, currentPage, pageSize, out totalRow);
        }

        public Design GetByKey(int id)
        {
            return _designRepository.GetSingleById(id);
        }

        public void Update(Design design)
        {
            _designRepository.Update(design);
        }
    }
}