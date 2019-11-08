using DOMAIN.Models;
using System.Collections.Generic;

namespace SERVICE.IService
{
    public interface IMeterialService : IBaseService
    {
        void CreateNew(Meterial meterial);

        void Update(Meterial meterial);

        void Delete(int id);

        bool CheckContain(string name);

        bool CheckContain(int id, string name);

        Meterial GetByKey(int id);

        IList<Meterial> GetByFilter(string keyWord, int currentPage, int pageSize, out int totalRow);
    }
}