using DefaultMvcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DefaultMvcProject.Controllers
{
    /// <summary>
    /// UserController
    /// </summary>
    public class UserController : Controller
    {
        private List<UserInfo> _userList = new List<UserInfo>()
        {
            new UserInfo() {UserId=1, UserName="李白",Email="libai@tang.com"},
            new UserInfo() {UserId=2, UserName="杜甫",Email="dufu@tang.com"},
            new UserInfo() {UserId=3, UserName="岑参",Email="censhen@tang.com"},
            new UserInfo() {UserId=4, UserName="王维",Email="wangwei@tang.com"},
        };

        /// <summary>
        /// 获取指定用户
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ActionResult GetUserById(int uid)
        {
            var userInfo = _userList.FirstOrDefault(u => u.UserId == uid);
            return Json(new { Flag = userInfo == null, Data = userInfo });
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUsers()
        {
            return Json(new { Flag = true, Data = _userList });
        }
        
        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public ActionResult SaveUser(UserInfo userInfo)
        {
            userInfo.UserId = _userList.Count + 1;
            _userList.Add(userInfo);
            return Json(new { Flag = true, Data = userInfo });
        }
    }
}