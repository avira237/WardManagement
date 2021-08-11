using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.Models;
using WM.Models.Repos;

namespace WM.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        [HttpGet, ActionName("Index")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getRole()
        {
            WMEntities context = new WMEntities();
            return Json(context.ROLEMASTERs.Select(x => new
            {
                ID = x.ID,
                ROLE_NAME = x.ROLE_NAME
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("RightDT")]
        public JsonResult RoleMasterDataTable()
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

                repoRoleMaster _repoRoleMaster = new repoRoleMaster();
                int roleid = int.Parse(Request.Form["ID"]);
                var _data = _repoRoleMaster.RightDT(roleid, searchVal, null, skip, length, sortBy, columnIndex);

                var data = new { Status = 1, data = _data, recordsTotal = repoRoleMaster.totalrows, recordsFiltered = repoRoleMaster.totalrows };


                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, ActionName("IsAddChange")]
        public ActionResult IsAddChangedata()
        {
            try
            {
                var check = bool.Parse(Request.Form["check"]);
                int primaryId = int.Parse(Request.QueryString["primaryId"]);
                new repoRoleMaster().Editaddcheckbox(check, primaryId);
                var data = new { Status = 1};
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, ActionName("IsUpdateChange")]
        public ActionResult IsUpdateChangedata()
        {
            try
            {
                var check = bool.Parse(Request.Form["check"]);
                int primaryId = int.Parse(Request.QueryString["primaryId"]);
                new repoRoleMaster().Editupdatecheckbox(check, primaryId);
                var data = new { Status = 1 };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, ActionName("IsViewChange")]
        public ActionResult IsViewChangedata()
        {
            try
            {
                var check = bool.Parse(Request.Form["check"]);
                int primaryId = int.Parse(Request.QueryString["primaryId"]);
                new repoRoleMaster().Editviewcheckbox(check, primaryId);
                var data = new { Status = 1 };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, ActionName("IsPrintChange")]
        public ActionResult IsPrintChangedata()
        {
            try
            {
                var check = bool.Parse(Request.Form["check"]);
                int primaryId = int.Parse(Request.QueryString["primaryId"]);
                new repoRoleMaster().Editprintcheckbox(check, primaryId);
                var data = new { Status = 1 };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, ActionName("IsDeleteChange")]
        public ActionResult IsDeleteChangedata()
        {
            try
            {
                var check = bool.Parse(Request.Form["check"]);
                int primaryId = int.Parse(Request.QueryString["primaryId"]);
                new repoRoleMaster().Editdeletecheckbox(check, primaryId);
                var data = new { Status = 1 };
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