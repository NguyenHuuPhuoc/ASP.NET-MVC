using COMMON.Helper;
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
    public class MeterialController : BaseController
    {
        private readonly IMeterialService _meTerialService;

        public MeterialController(IErrorService errorService, IMeterialService meTerialService) : base(errorService)
        {
            this._meTerialService = meTerialService;
        }

        // GET: Admin/Meterial
        public ActionResult Index(MeterialViewModel model, int? page)
        {
            var currentPage = page.HasValue ? page.Value - 1 : 0;
            var pageSize = 10;
            int total = 0;
            var lst = _meTerialService.GetByFilter(model.KeyWord, currentPage, pageSize, out total);
            model.Meterials = new PagedList<Meterial>(lst, currentPage, pageSize, total);

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateNew()
        {
            var model = new Meterial();
            model.Status = true;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNew(Meterial model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var check = _meTerialService.CheckContain(model.Name);
                    if (check)
                    {
                        AlertWarning(InfoString.SetContainString("Tên"));
                        return View(model);
                    }

                    model.CreateDate = DateTime.Now;
                    model.CreateBy = "hadacduong";
                    _meTerialService.CreateNew(model);
                    _meTerialService.CommitChanges();
                    AlertSuccess(InfoString.CREATE_SUCCESSFULL);
                }
                catch (Exception ex)
                {
                    Log(ex);
                    AlertError(InfoString.ERROR_SYSTEM);
                }
                return RedirectToAction("Index");
            }
            AlertWarning(InfoString.INVALID_INFO);
            return View(model);
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var model = _meTerialService.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Meterial model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var flag = _meTerialService.CheckContain(model.Id, model.Name);
                    if (flag)
                    {
                        AlertWarning(InfoString.SetContainString("Tên"));
                        return View(model);
                    }

                    model.UpdateDate = DateTime.Now;
                    model.UpdateBy = "hadacduong";
                    _meTerialService.Update(model);
                    _meTerialService.CommitChanges();
                    AlertSuccess(InfoString.UPDATE_SUCCESSFULL);
                }
                catch (Exception ex)
                {
                    Log(ex);
                    AlertError(InfoString.ERROR_SYSTEM);
                }

                return RedirectToAction("Index");
            }
            AlertWarning(InfoString.INVALID_INFO);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _meTerialService.Delete(id);
                _meTerialService.CommitChanges();
                AlertSuccess(InfoString.DELETE_SUCCESSFULL);
            }
            catch (Exception ex)
            {
                Log(ex);
                AlertError(InfoString.ERROR_SYSTEM);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ViewDetail(int id)
        {
            var vm = new MeterialModelDetail();
            var model = _meTerialService.GetByKey(id);
            model.UpdateModelToViewModel(vm);
            return Json(new
            {
                data = vm
            });
        }
    }
}