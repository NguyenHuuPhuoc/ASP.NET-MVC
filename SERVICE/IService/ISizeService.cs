using DOMAIN.Models;
using System.Collections.Generic;

namespace SERVICE.IService
{
    public interface ISizeService : IBaseService
    {
        void CreateNew(Size size);

        void Update(Size size);

        void Delete(int id);

        Size GetByKey(int id);

        IList<Size> GetByFilter(double? width, double? height, string unit, int currentPage, int pageSize, out int totalRow);

        bool CheckContain(double width, double height, string unit);

        bool CheckContain(int id, double width, double height, string unit);
    }
}