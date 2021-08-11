using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.Models.Repos;
using WM.Filter;
namespace WM.Controllers
{
    public class UserController : Controller
    {
        [authenticationfilter]
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost, ActionName("UserMasterDT")]
        public JsonResult PersonDataTable()
        {
            try
            {
                var startRecords = Request.Form["start"];
                var lengthRecords = Request.Form["length"];

                int skip = (startRecords != null) ? int.Parse(startRecords) : 0;
                int length = (lengthRecords != null) ? int.Parse(lengthRecords) : 10;
                string searchVal = Request.Form["search[value]"];
                string sortBy = Request.Form["order[0][dir]"];
                int columnIndex = !Request.Form.AllKeys.Contains("order[0][column]") ? 0 : int.Parse(Request.Form["order[0][column]"]);

                repoUserMaster _repoUserMaster = new repoUserMaster();
                var _data = _repoUserMaster.UserMasterDT(searchVal, null, skip, length, sortBy, columnIndex);

                var data = new { Status = 1, data = _data, recordsTotal = repoPerson.totalrows, recordsFiltered = repoPerson.totalrows };


                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var data = new { Status = 0, data = "", msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Getdataid()
        {
            try
            {
                int primaryId = int.Parse(Request.QueryString["primaryId"]);
                var _data = new repoUserMaster().UserMasterDT("", primaryId, 0, 1, "asc", 0);
                var data = new { data = _data, Status = 1, msg = "Record fetched." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult editgetroledata()
        {
            try
            {
                repoUserMaster _repoUserMaster = new repoUserMaster();
                int ROLEID = int.Parse(Request.Form["ROLEID"]);
                var _data = _repoUserMaster.getrole(ROLEID);
                var data = new { data = _data, Status = 1, msg = "Data fetched." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [authenticationfilter]
        [HttpPost, ActionName("SaveUser")]
        public JsonResult SaveUserdetails()
        {
            try
            {
                new repoUserMaster().SaveUserMaster(Request);
                var data = new { Status = 1, msg = "Record Saved Successfully." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [authenticationfilter]
        [HttpPost, ActionName("UpdateUser")]
        public JsonResult UpdateUsermaster()
        {
            try
            {
                new repoUserMaster().EdituserMaster(Request);
                var data = new { Status = 1, msg = "Record edit Successfully." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getuserid()
        {
            try
            {
                repoUserMaster _repoUserMaster = new repoUserMaster();
                var id=Request.QueryString["uid"];
                var _data = _repoUserMaster.getuserid(id);
                var data = new { data = _data, Status = 1, msg = "Data fetched." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}