using DOMAIN.Models;
using System.Collections.Generic;

namespace SERVICE.IService
{
    public interface IDesignService : IBaseService
    {
        void CreateNew(Design design);

        void Update(Design design);

        void Delete(int id);

        Design GetByKey(int id);

        bool CheckContain(string name);

        bool CheckContain(int id, string name);

        IList<Design> GetByFilter(string keyWord, int? categoryParentId, int currentPage, int pageSize, out int totalRow);
    }
}