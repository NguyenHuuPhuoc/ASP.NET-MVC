using DOMAIN.Models;
using MvcPaging;
using SERVICE.IService;
using ShopManager.ModelExtensions;
using ShopManager.Models;
using System;
using System.Web.Mvc;

namespace ShopManager.Controllers
{
    [Authorize]
    public class UnitController : BaseController
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService, IErrorService errorService) : base(errorService)
        {
            this._unitService = unitService;
        }

        // GET: Admin/Unit
        public ActionResult Index(UnitViewModel model, int? page)
        {
            var currentPage = page.HasValue ? page.Value - 1 : 0;
            var pageSize = 10;
            int total = 0;
            var lst = _unitService.GetByFilter(model.KeyWord, currentPage, pageSize, out total);
            model.Units = new PagedList<Unit>(lst, currentPage, pageSize, total);

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateNew()
        {
            var model = new Unit();
            model.Status = true;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNew(Unit model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var check = _unitService.CheckContain(model.Name);
                    if (check)
                    {
                        AlertWarning("Tên đợn vị này đã tồn tại, hãy thử lại với tên khác.");
                        return View(model);
                    }

                    model.CreateDate = DateTime.Now;
                    model.CreateBy = "hadacduong";
                    _unitService.CreateNew(model);
                    _unitService.CommitChanges();
                    AlertSuccess("Thêm mới đơn vị thành công.");
                }
                catch (Exception ex)
                {
                    Log(ex);
                    AlertError("Có lỗi xẩy ra trong quá trình xử lý, hãy thử lại.");
                }
                return RedirectToAction("Index");
            }
            AlertWarning("Dữ liệu đang sai định dạng, hãy kiểm tra lại dữ liệu.");
            return View(model);
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var model = _unitService.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Unit model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var flag = _unitService.CheckContain(model.Id, model.Name);
                    if (flag)
                    {
                        AlertWarning("Tên này đã tồn tại, hãy thử lại với tên khác.");
                        return View(model);
                    }

                    model.UpdateDate = DateTime.Now;
                    model.UpdateBy = "hadacduong";
                    _unitService.Update(model);
                    _unitService.CommitChanges();
                    AlertSuccess("Cấp nhật đơn vị tính thành công.");
                }
                catch (Exception ex)
                {
                    Log(ex);
                    AlertError("Có lỗi xẩy ra trong quá trình xử lý, hãy thử lại.");
                }

                return RedirectToAction("Index");
            }
            AlertWarning("Dữ liệu đang không đúng định dạng, hãy kiểm tra lại dữ liệu.");
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _unitService.Delete(id);
                _unitService.CommitChanges();
                AlertSuccess("Xóa thành công.");
            }
            catch (Exception ex)
            {
                Log(ex);
                AlertError("Xóa thất bại, có lỗi xẩy ra trong quá trình xử lý.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ViewDetail(int id)
        {
            var vm = new UnitModelDetail();
            var model = _unitService.GetByKey(id);
            model.UpdateModelToViewModel(vm);
            return Json(new
            {
                data = vm
            });
        }
    }
}