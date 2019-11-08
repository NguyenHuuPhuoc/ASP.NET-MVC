using DATAACCESS.IRepository;
using DOMAIN.Models;
using Infrastructure.UnitOfWork;
using SERVICE.IService;
using System.Collections.Generic;

namespace SERVICE.ServiceImp
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._categoryRepository = categoryRepository;
        }

        public bool CheckContain(string code)
        {
            return _categoryRepository.CheckContains(n => n.Code.Equals(code));
        }

        public bool CheckContain(int id, string code)
        {
            return _categoryRepository.CheckContains(n => n.Id != id && n.Code.Equals(code));
        }

        public void CreateNew(Category category)
        {
            _categoryRepository.CreateNew(category);
        }

        public void Delete(int id)
        {
            _categoryRepository.Delete(id);
        }

        public IList<Category> GetByfilter(string keyWord, int? parentId, int? typeId, int currentPage, int pageSize, out int totalRow)
        {
            return _categoryRepository.GetByfilter(keyWord, parentId, typeId, currentPage, pageSize, out totalRow);
        }

        public Category GetByKey(int id)
        {
            return _categoryRepository.GetSingleById(id);
        }

        public IEnumerable<Category> GetParents()
        {
            return _categoryRepository.GetMulti(n => n.ParentId == null);
        }

        public IEnumerable<Category> GetParents(int typeId)
        {
            return _categoryRepository.GetMulti(n => n.ParentId == null && n.Type == typeId);
        }

        public void Update(Category category)
        {
            _categoryRepository.Update(category);
        }
    }
}