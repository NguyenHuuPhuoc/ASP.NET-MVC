using ASPNetIdentityManagment.IService;
using ASPNetIdentityManagment.ServiceImp;
using COMMON.Helper;
using DOMAIN.Models;
using MvcPaging;
using SERVICE.IService;
using ShopManager.App_Start;
using ShopManager.ModelExtensions;
using ShopManager.Models;
using System;
using System.Web.Mvc;

namespace ShopManager.Controllers
{
    [Authorize]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(IErrorService errorService, ICategoryService categoryService) : base(errorService)
        {
            this._categoryService = categoryService;
        }

        // GET: Admin/Category
        [RBACAuthorized(Roles = "VIEW_CATEGORY")]
        public ActionResult Index(CategoryViewModel model, int? page)
        {
            var currentPage = page.HasValue ? page.Value - 1 : 0;
            var pageSize = 10;
            int total = 0;
            var lst = _categoryService.GetByfilter(model.KeyWord, model.ParentId, model.TypeId, currentPage, pageSize, out total);
            model.Categories = new PagedList<Category>(lst, currentPage, pageSize, total);
            ViewData["CategoryTypes"] = COMMON.Helper.TypeCategory.GetTypes();
            if (model.TypeId.HasValue)
            {
                model.Parents = _categoryService.GetParents(model.TypeId.Value);
            }
            else
            {
                model.Parents = _categoryService.GetParents();
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateNew(int? parentId, int? typeId)
        {
            var model = new Category();
            model.Status = true;
            model.Code = Guid.NewGuid().ToString().ToUpper().Substring(0, 5);
            ViewData["CategoryTypes"] = COMMON.Helper.TypeCategory.GetTypes();
            if (typeId.HasValue)
            {
                ViewData["CategoryParents"] = _categoryService.GetParents(typeId.Value);
                model.Type = typeId.Value;
            }
            else
            {
                model.Type = 1;
                ViewData["CategoryParents"] = _categoryService.GetParents(model.Type);
            }
            if (parentId.HasValue)
            {
                model.ParentId = parentId.Value;
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateNew(Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var flagCode = _categoryService.CheckContain(model.Code);
                    if (flagCode)
                    {
                        AlertWarning(InfoString.SetContainString("Mã danh mục"));
                        ViewData["CategoryTypes"] = COMMON.Helper.TypeCategory.GetTypes();
                        ViewData["CategoryParents"] = _categoryService.GetParents(model.Type);
                        return View(model);
                    }
                    model.CreateBy = GetCurrentInstance().UserName;
                    model.CreateDate = DateTime.Now;
                    _categoryService.CreateNew(model);
                    _categoryService.CommitChanges();
                    AlertSuccess(InfoString.CREATE_SUCCESSFULL);
                }
                catch (Exception ex)
                {
                    Log(ex);
                    AlertError(InfoString.ERROR_SYSTEM);
                }

                return RedirectToAction("Index", "Category", new { ParentId = model.ParentId, TypeId = model.Type });
            }
            AlertWarning(InfoString.INVALID_INFO);
            ViewData["CategoryTypes"] = COMMON.Helper.TypeCategory.GetTypes();
            ViewData["CategoryParents"] = _categoryService.GetParents(model.Type);
            return View(model);
        }

        [HttpPost]
        public JsonResult SetParentsByTypeId(int typeId)
        {
            var lst = _categoryService.GetParents(typeId);

            return Json(new
            {
                data = lst
            });
        }

        [HttpPost]
        public JsonResult SetCode(string code)
        {
            var c = _categoryService.StandardizedCode(code).ToUpper();
            return Json(new
            {
                data = c
            });
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var model = _categoryService.GetByKey(id);
            ViewData["CategoryTypes"] = COMMON.Helper.TypeCategory.GetTypes();
            ViewData["CategoryParents"] = _categoryService.GetParents(model.Type);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update(Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var flagCode = _categoryService.CheckContain(model.Id, model.Code);
                    if (flagCode)
                    {
                        AlertWarning(InfoString.SetContainString("Mã danh mục"));
                        ViewData["CategoryTypes"] = COMMON.Helper.TypeCategory.GetTypes();
                        ViewData["CategoryParents"] = _categoryService.GetParents(model.Type);
                        return View(model);
                    }
                    model.UpdateBy = "hadacduong";
                    model.UpdateDate = DateTime.Now;
                    _categoryService.Update(model);
                    _categoryService.CommitChanges();
                    AlertSuccess(InfoString.UPDATE_SUCCESSFULL);
                }
                catch (Exception ex)
                {
                    Log(ex);
                    AlertError(InfoString.ERROR_SYSTEM);
                }

                return RedirectToAction("Index", "Category", new { ParentId = model.ParentId, TypeId = model.Type });
            }
            AlertWarning(InfoString.INVALID_INFO);
            ViewData["CategoryTypes"] = COMMON.Helper.TypeCategory.GetTypes();
            ViewData["CategoryParents"] = _categoryService.GetParents(model.Type);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _categoryService.Delete(id);
                _categoryService.CommitChanges();
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
        public ActionResult ViewDetail(int id)
        {
            var model = _categoryService.GetByKey(id);
            var vm = new CategoryModelDetail();
            model.UpdateModelToViewModel(vm);
            if (model.ParentId.HasValue)
            {
                vm.Parent = _categoryService.GetByKey(model.ParentId.Value).Name;
            }
            else
            {
                vm.Parent = "Danh mục cha";
            }

            return Json(new
            {
                data = vm
            });
        }
    }
}