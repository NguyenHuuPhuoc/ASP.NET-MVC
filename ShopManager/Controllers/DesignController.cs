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
    public class DesignController : BaseController
    {
        private readonly IDesignService _designService;
        private readonly ICategoryService _categoryService;

        public DesignController(IErrorService errorService, IDesignService designService, ICategoryService categoryService) : base(errorService)
        {
            this._designService = designService;
            this._categoryService = categoryService;
        }

        // GET: Admin/Design
        public ActionResult Index(DesignViewModel model, int? page)
        {
            var currentPage = page.HasValue ? page.Value - 1 : 0;
            var pageSize = 10;
            int total = 0;
            var lst = _designService.GetByFilter(model.KeyWord, model.ParentCategoryId, currentPage, pageSize, out total);
            model.Designs = new PagedList<Design>(lst, currentPage, pageSize, total);
            ViewData["ParentCategories"] = _categoryService.GetParents(1); //parent type production

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateNew(int? parentCategoryId)
        {
            var model = new Design();
            model.Status = true;
            if (parentCategoryId.HasValue)
            {
                model.ParentCategoryId = parentCategoryId.Value;
            }
            ViewData["ParentCategories"] = _categoryService.GetParents(1);//parent type production
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateNew(Design model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var flag = _designService.CheckContain(model.Name);
                    if (flag)
                    {
                        AlertWarning(InfoString.SetContainString("Tên"));
                        ViewData["ParentCategories"] = _categoryService.GetParents(1);//parent type production
                        return View(model);
                    }

                    model.CreateBy = "hadacduong";
                    model.CreateDate = DateTime.Now;
                    _designService.CreateNew(model);
                    _designService.CommitChanges();
                    AlertSuccess(InfoString.CREATE_SUCCESSFULL);
                }
                catch (Exception ex)
                {
                    Log(ex);
                    AlertError(InfoString.ERROR_SYSTEM);
                }

                return RedirectToAction("Index", "Design", new { ParentCategoryId = model.ParentCategoryId });
            }
            AlertWarning(InfoString.INVALID_INFO);
            ViewData["ParentCategories"] = _categoryService.GetParents(1);//parent type production
            return View(model);
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var model = _designService.GetByKey(id);
            ViewData["ParentCategories"] = _categoryService.GetParents(1); //parent type production
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update(Design model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var flag = _designService.CheckContain(model.Id, model.Name);
                    if (flag)
                    {
                        AlertWarning(InfoString.SetContainString("Tên"));
                        ViewData["ParentCategories"] = _categoryService.GetParents(1);//parent type production
                        return View(model);
                    }

                    model.UpdateBy = "hadacduong";
                    model.UpdateDate = DateTime.Now;
                    _designService.Update(model);
                    _designService.CommitChanges();
                    AlertSuccess(InfoString.UPDATE_SUCCESSFULL);
                }
                catch (Exception ex)
                {
                    Log(ex);
                    AlertError(InfoString.ERROR_SYSTEM);
                }

                return RedirectToAction("Index", "Design", new { ParentCategoryId = model.ParentCategoryId });
            }
            AlertWarning(InfoString.INVALID_INFO);
            ViewData["ParentCategories"] = _categoryService.GetParents(1);//parent type production
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _designService.Delete(id);
                _designService.CommitChanges();
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
            var model = _designService.GetByKey(id);
            var vm = new DesignModelDetail();
            model.UpdateModelToViewModel(vm);
            vm.ParentCategory = _categoryService.GetByKey(model.ParentCategoryId).Name;
            return Json(new
            {
                data = vm
            });
        }
    }
}