using DOMAIN.Models;
using System.Collections.Generic;

namespace SERVICE.IService
{
    public interface IUnitService : IBaseService
    {
        void CreateNew(Unit unit);

        void Update(Unit unit);

        void Delete(int Id);

        Unit GetByKey(int Id);

        bool CheckContain(string name);

        bool CheckContain(int id, string name);

        IList<Unit> GetByFilter(string keyWord, int currentPage, int pageSize, out int totalRow);
    }
}