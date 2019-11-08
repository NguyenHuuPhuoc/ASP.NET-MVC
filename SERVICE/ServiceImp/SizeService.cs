using DATAACCESS.IRepository;
using DOMAIN.Models;
using Infrastructure.UnitOfWork;
using SERVICE.IService;
using System.Collections.Generic;

namespace SERVICE.ServiceImp
{
    public class SizeService : BaseService, ISizeService
    {
        private readonly ISizeRepository _sizeRepository;

        public SizeService(IUnitOfWork unitOfWork, ISizeRepository sizeRepository) : base(unitOfWork)
        {
            this._sizeRepository = sizeRepository;
        }

        public bool CheckContain(double width, double height, string unit)
        {
            return _sizeRepository.CheckContains(n => n.Width == width && n.Height == height && n.Unit.Equals(unit));
        }

        public bool CheckContain(int id, double width, double height, string unit)
        {
            return _sizeRepository.CheckContains(n => n.Id != id && n.Width == width && n.Height == height && n.Unit.Equals(unit));
        }

        public void CreateNew(Size size)
        {
            _sizeRepository.CreateNew(size);
        }

        public void Delete(int id)
        {
            _sizeRepository.Delete(id);
        }

        public IList<Size> GetByFilter(double? width, double? height, string unit, int currentPage, int pageSize, out int totalRow)
        {
            return _sizeRepository.GetByFilter(width, height, unit, currentPage, pageSize, out totalRow);
        }

        public Size GetByKey(int id)
        {
            return _sizeRepository.GetSingleById(id);
        }

        public void Update(Size size)
        {
            _sizeRepository.Update(size);
        }
    }
}