﻿using ASPNetIdentityManagment.IService;
using COMMON.Helper;
using ContextConnection.ContextDB;
using Microsoft.AspNet.Identity.Owin;
using MvcPaging;
using SERVICE.IService;
using ShopManager.App_Start;
using ShopManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShopManager.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IGroupService _groupService;
        private ApplicationUserManager _userManager;

        public UserController(IErrorService errorService, IGroupService groupService, ApplicationUserManager userManager) : base(errorService)
        {
            this._groupService = groupService;
            _userManager = userManager;
        }

        // GET: User
        public ActionResult Index(UserViewModel model, int? page)
        {
            var currentPage = page.HasValue ? page.Value - 1 : 0;
            var pageSize = 10;
            var total = 0;
            var lst = GetByFilter(model.KeyWord, currentPage, pageSize, out total);
            model.Users = new PagedList<ApplicationUser>(lst, currentPage, pageSize, total);
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateNew()
        {

            var groups = _groupService.GetAll();
            var lstGroupDetail = new List<GroupVM>();
            var user = new ApplicationUser();
            user.Id = Guid.NewGuid().ToString();
            foreach (var item in groups)
            {
                lstGroupDetail.Add(new GroupVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    IsCheck = false,
                    Descreption = item.Descreption
                });
            }
            var model = new UserModelDetail();
            model.User = user;
            model.Groups = lstGroupDetail;
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateNew(UserModelDetail model)
        {
            if (!model.Groups.Any(n => n.IsCheck == true))
            {
                AlertWarning("Người dùng này chưa thuộc nhóm người dùng nào cả, hãy chọn nhóm của người dùng.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var checkUser = await _userManager.FindByNameAsync(model.User.UserName);
                    if (checkUser != null)
                    {
                        AlertWarning(InfoString.SetContainString("Tên đăng nhập"));
                        return View(model);
                    }

                    var userByEmail = await _userManager.FindByEmailAsync(model.User.Email);
                    if (userByEmail != null)
                    {
                        AlertWarning(InfoString.SetContainString("Email"));
                        return View(model);
                    }
                    model.User.CreateBy = GetCurrentInstance().UserName;
                    model.User.CreateDate = DateTime.Now;
                    var result = await _userManager.CreateAsync(model.User, model.User.PasswordHash);
                    if (result.Succeeded)
                    {
                        //add user to group
                        foreach (var item in model.Groups)
                        {
                            if (item.IsCheck)
                            {
                                _groupService.AddUserToGroup(model.User.Id, item.Id);
                            }
                        }
                        _groupService.CommitChanges();
                        AlertSuccess(InfoString.CREATE_SUCCESSFULL);
                    }
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
        public async Task<ActionResult> Update(string id)
        {
            var groups = _groupService.GetAll();
            var lstGroupDetail = new List<GroupVM>();
            var user = await _userManager.FindByIdAsync(id);
            var groupsOfUser = _groupService.GetGroupIdsByUserId(user.Id);
            foreach (var item in groups)
            {
                var g = new GroupVM();
                g.Id = item.Id;
                g.Name = item.Name;
                g.IsCheck = false;
                g.Descreption = item.Descreption;
                if (groupsOfUser.Any(n => n.GroupId == item.Id))
                {
                    g.IsCheck = true;
                }

                lstGroupDetail.Add(g);
            }
            var model = new UserModelDetail();
            model.User = user;
            model.Groups = lstGroupDetail;
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UserModelDetail model)
        {
            if (!model.Groups.Any(n => n.IsCheck == true))
            {
                AlertWarning("Người dùng này chưa thuộc nhóm người dùng nào cả, hãy chọn nhóm của người dùng.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                _groupService.BeginTran();
                try
                {
                    var userFindByEmail = await _userManager.FindByEmailAsync(model.User.Email);
                    if (userFindByEmail != null && !userFindByEmail.Id.Equals(model.User.Id))
                    {
                        AlertWarning(InfoString.SetContainString("Email"));
                        return View(model);
                    }
                    var userNeedUpdate = await _userManager.FindByIdAsync(model.User.Id);
                    userNeedUpdate.FullName = model.User.FullName;
                    userNeedUpdate.Email = model.User.Email;
                    userNeedUpdate.BirthDay = model.User.BirthDay;
                    userNeedUpdate.Status = model.User.Status;
                    userNeedUpdate.UpdateBy = GetCurrentInstance().UserName;
                    userNeedUpdate.UpdateDate = DateTime.Now;
                    var result = await _userManager.UpdateAsync(userNeedUpdate);
                    if (result.Succeeded)
                    {
                        //delete oder group of user
                        _groupService.DeleteGroupOfUser(model.User.Id);
                        _groupService.CommitChanges();

                        //add user to new groups
                        foreach (var item in model.Groups)
                        {
                            if (item.IsCheck)
                            {
                                _groupService.AddUserToGroup(model.User.Id, item.Id);
                            }
                        }
                        _groupService.CommitChanges();
                        _groupService.CommitTran();
                        AlertSuccess(InfoString.UPDATE_SUCCESSFULL);
                    }
                }
                catch (Exception ex)
                {
                    Log(ex);
                    AlertError(InfoString.ERROR_SYSTEM);
                    _groupService.RollbackTran();
                }
                return RedirectToAction("Index");
            }
            AlertWarning(InfoString.INVALID_INFO);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            _groupService.BeginTran();
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user.UserName.Equals(GetCurrentInstance().UserName))
                {
                    AlertWarning("Tài khoản này đang sử dụng hiện hành, hãy dùng tài khoản có quyền xóa khác để xóa.");
                    return RedirectToAction("Index");
                }

                _groupService.DeleteGroupOfUser(id);
                _groupService.CommitChanges();

                await _userManager.DeleteAsync(user);
                _groupService.CommitTran();
                AlertSuccess(InfoString.DELETE_SUCCESSFULL);
            }
            catch (Exception ex)
            {
                Log(ex);
                AlertError(InfoString.ERROR_SYSTEM);
                _groupService.RollbackTran();
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<JsonResult> ViewDetail(string id)
        {
            var currentUser = await _userManager.FindByIdAsync(id);


            return Json(new
            {
                userName = currentUser.UserName,
                fullName = currentUser.FullName,
                email = currentUser.Email,
                createDate = currentUser.CreateDate.Value.ToString("dd-MM-yyyy hh:mm:ss"),
                createBy = currentUser.CreateBy,
                updateDate = currentUser.UpdateDate.HasValue ? currentUser.UpdateDate.Value.ToString("dd-MM-yyyy hh:mm:ss") : string.Empty,
                updateBy = currentUser.UpdateBy,
                status = currentUser.Status ? "Kích hoạt" : "Tạm khóa"
            });
        }

        private IList<ApplicationUser> GetByFilter(string keyWord, int currentPage, int pageSize, out int total)
        {
            var query = from a in _userManager.Users select a;

            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                query = query.Where(n => n.UserName.Contains(keyWord) || n.Email.Contains(keyWord));
            }
            total = query.Count();
            query = query.OrderBy(n => n.UserName);
            var lst = query.Skip(currentPage * pageSize).Take(pageSize).ToList();
            return lst;
        }
    }
}