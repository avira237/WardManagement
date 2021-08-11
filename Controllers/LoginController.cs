using IMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.Models;
using WM.Filter;


namespace WM.Controllers
{
    public class LoginController : Controller
    {
      
        // GET: Login
        public ActionResult Index()
        {
            if (Session["userID"] != null)
            {
                return RedirectToAction("Index", "WardMaster", new { FileOperation = "View", Resource = "WARDMASTER" });
            }
            else
            {
                return View("frmlogin");
            }
        }

        [HttpPost]
        public JsonResult Loginuser()
        {

            using (WMEntities db = new WMEntities())
            {
                string encpass = Enc.Encrypt(Request.Form["PASSWORD"]);
                var id = Request.Form["USER_ID"];
                var data = (from c in db.USERMASTERs
                            where c.USER_ID == id
                            where c.PASSWORD == encpass
                            join x in db.ROLEMASTERs
                            on c.ROLE_ID equals x.ID
                            select new
                            {
                                PrimaryId = c.ID,
                                UserId = c.USER_ID,
                                RoleId = c.ROLE_ID,
                                RoleName = x.ROLE_NAME,
                                IsLocked=c.IS_LOCKED
                            }).SingleOrDefault();
                if (data != null)
                {
                    Session["userID"] = data.UserId;
                    Session["roleID"] = data.RoleId;
                    Session["roleName"] = data.RoleName;
                    Session["Uid"] = data.PrimaryId;
                    var _data = new {data=data, redirecturl= "/WardMaster/Index?FileOperation=View&Resource=WARDMASTER", Status = 1};
                    return Json(_data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var _data = new { Status = 0, msg ="login failed.."};
                    return Json(_data, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult AccessDenied()
        {
            Response.StatusCode = 302;
            return View("Forbidden");
        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}