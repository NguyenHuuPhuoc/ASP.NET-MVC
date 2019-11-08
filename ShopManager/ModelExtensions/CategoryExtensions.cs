using COMMON.Helper;
using DOMAIN.Models;
using ShopManager.Models;
using System.Linq;

namespace ShopManager.ModelExtensions
{
    public static class CategoryExtensions
    {
        public static void UpdateModelToViewModel(this Category model, CategoryModelDetail vm)
        {
            vm.Id = model.Id;
            vm.Name = model.Name;
            vm.Code = model.Code;
            vm.Descreption = model.Descreption;
            vm.Type = TypeCategory.GetTypes().FirstOrDefault(n => n.TypeId == model.Type).Name;
            if (model.Status)
            {
                vm.Status = "Kích hoạt";
            }
            else
            {
                vm.Status = "Tạm khóa";
            }

            vm.CreateDate = model.CreateDate.Value.ToString("dd/MM/yyyy hh:mm:ss");
            vm.CreateBy = model.CreateBy;
            if (model.UpdateDate.HasValue)
            {
                vm.UpdateDate = model.UpdateDate.Value.ToString("dd/MM/yyyy hh:mm:ss");
            }
            vm.UpdateBy = model.UpdateBy;
        }
    }
}