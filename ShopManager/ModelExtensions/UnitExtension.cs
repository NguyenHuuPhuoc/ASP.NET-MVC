using DOMAIN.Models;
using ShopManager.Models;

namespace ShopManager.ModelExtensions
{
    public static class UnitExtension
    {
        /// <summary>
        /// Out UnitModelDetail - viewmodel when have model
        ///
        /// same mapping 2 model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="vm"> view model - UnitModelDetail</param>
        public static void UpdateModelToViewModel(this Unit model, UnitModelDetail vm)
        {
            vm.Id = model.Id;

            vm.Name = model.Name;

            vm.Descreption = model.Descretion;

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