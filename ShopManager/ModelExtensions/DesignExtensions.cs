using DOMAIN.Models;
using ShopManager.Models;

namespace ShopManager.ModelExtensions
{
    public static class DesignExtensions
    {
        public static void UpdateModelToViewModel(this Design model, DesignModelDetail vm)
        {
            vm.Id = model.Id;

            vm.Name = model.Name;

            vm.Descreption = model.Descreption;

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