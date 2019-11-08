using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SERVICE.IService;
using ASPNetIdentityManagment.IService;
using ShopManager.Models;
using DOMAIN.Models;
using MvcPaging;
using COMMON.Helper;

namespace ShopManager.Controllers
{
    [Authorize]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IErrorService errorService, IRoleService roleService) : base(errorService)
        {
            this._roleService = roleService;
        }

        // GET: Role
        public ActionResult Index(RoleViewModel model, int? page)
        {
            var currentPage = page.HasValue ? page.Value - 1 : 0;
            var pageSize = 10;
            int total = 0;
            var lst = _roleService.GetByFilter(model.KeyWord, currentPage, pageSize, out total);
            model.AppRoles = new PagedList<Role>(lst, currentPage, pageSize, total);

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateNew()
        {
            var model = new Role();
            model.Status = true;

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateNew(Role model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var check = _roleService.CheckContain(model.Name);
                    if (check)
                    {
                        AlertWarning(InfoString.SetContainString("Tên quyền"));
                        return View(model);
                    }

                    model.CreateDate = DateTime.Now;
                    model.CreateBy = GetCurrentInstance().UserName;
                    _roleService.CreateNew(model);
                    _roleService.CommitChanges();
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
            var model = _roleService.GetByKey(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Update(Role model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var check = _roleService.CheckContain(model.Id, model.Name);
                    if (check)
                    {
                        AlertWarning(InfoString.SetContainString("Tên quyền"));
                        return View(model);
                    }

                    model.UpdateDate = DateTime.Now;
                    model.UpdateBy = GetCurrentInstance().UserName;
                    _roleService.Update(model);
                    _roleService.CommitChanges();
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
        public JsonResult ViewDetail(int id)
        {
            var model = _roleService.GetByKey(id);
            return Json(new
            {
                roleName = model.Name,
                descreption = model.Descreption,
                createDate = model.CreateDate.Value.ToString("dd-MM-yyyy hh:mm:ss"),
                createBy = model.CreateBy,
                updateDate = model.UpdateDate.HasValue ? model.UpdateDate.Value.ToString("dd-MM-yyyy hh:mm:ss") : string.Empty,
                updateBy = model.UpdateBy,
                status = model.Status ? "Kích hoạt" : "Tạm khóa"
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var flag = _roleService.CheckRoleInAnyGroup(id);
                if (flag)
                {
                    AlertInfo("Chức năng này đang trong 1 nhóm người dùng nào đó, xóa thất bại.");
                }
                else
                {
                    _roleService.Delete(id);
                    _roleService.CommitChanges();
                    AlertSuccess(InfoString.DELETE_SUCCESSFULL);
                }
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