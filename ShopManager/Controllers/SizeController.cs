using COMMON.Helper;
using DOMAIN.Models;
using MvcPaging;
using SERVICE.IService;
using ShopManager.Models;
using System;
using System.Web.Mvc;

namespace ShopManager.Controllers
{
    [Authorize]
    public class SizeController : BaseController
    {
        private readonly ISizeService _sizeService;

        public SizeController(IErrorService errorService, ISizeService sizeService) : base(errorService)
        {
            this._sizeService = sizeService;
        }

        // GET: Admin/Size
        public ActionResult Index(SizeViewModel model, int? page)
        {
            var currentPage = page.HasValue ? page.Value - 1 : 0;
            var pageSize = 10;
            int total = 0;
            var lst = _sizeService.GetByFilter(model.Width, model.Height, model.Unit, currentPage, pageSize, out total);
            model.Sizes = new PagedList<Size>(lst, currentPage, pageSize, total);
            ViewData["UnitOfSize"] = UnitOfSizeCategory.GetUnitsOfSize();
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateNew()
        {
            var model = new Size();
            model.Status = true;
            ViewData["UnitOfSize"] = UnitOfSizeCategory.GetUnitsOfSize();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateNew(Size model)
        {
            try
            {
                bool check = _sizeService.CheckContain(model.Width, model.Height, model.Unit);
                if (check)
                {
                    AlertWarning(InfoString.SetContainString("Kích thước"));
                    ViewData["UnitOfSize"] = UnitOfSizeCategory.GetUnitsOfSize();
                    return View(model);
                }

                model.CreateBy = "hadacduong";
                model.CreateDate = DateTime.Now;
                _sizeService.CreateNew(model);
                _sizeService.CommitChanges();
                AlertSuccess(InfoString.CREATE_SUCCESSFULL);
            }
            catch (Exception ex)
            {
                Log(ex);
                AlertError(InfoString.ERROR_SYSTEM);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var model = _sizeService.GetByKey(id);
            ViewData["UnitOfSize"] = UnitOfSizeCategory.GetUnitsOfSize();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update(Size model)
        {
            try
            {
                bool check = _sizeService.CheckContain(model.Id, model.Width, model.Height, model.Unit);
                if (check)
                {
                    AlertWarning(InfoString.SetContainString("Kích thước"));
                    ViewData["UnitOfSize"] = UnitOfSizeCategory.GetUnitsOfSize();
                    return View(model);
                }

                model.UpdateBy = "hadacduong";
                model.UpdateDate = DateTime.Now;
                _sizeService.Update(model);
                _sizeService.CommitChanges();
                AlertSuccess(InfoString.UPDATE_SUCCESSFULL);
            }
            catch (Exception ex)
            {
                Log(ex);
                AlertError(InfoString.ERROR_SYSTEM);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _sizeService.Delete(id);
                _sizeService.CommitChanges();
                AlertSuccess(InfoString.DELETE_SUCCESSFULL);
            }
            catch (Exception ex)
            {
                Log(ex);
                AlertError(InfoString.ERROR_SYSTEM);
            }
            return RedirectToAction("Index");
        }
    }
}