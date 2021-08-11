using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.Filter;
using WM.Models;
using WM.Models.Repos;

namespace WM.Controllers
{
    public class WardMasterController : Controller
    {
        // GET: WardMaster
        [authenticationfilter]
        [HttpGet, ActionName("Index")]
        public ActionResult Index()
        {
            return View("Index");
        }
        [authenticationfilter]
        [HttpPost, ActionName("SaveWardMaster")]
        public JsonResult SaveWardMasterDetails()
        {
            try
            {
                new repoWardMaster().SaveWardMaster(Request);
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
        [HttpPost, ActionName("editWardMaster")]
        public JsonResult editWardMasterDetails()
        {
            try
            {
                new repoWardMaster().editWardMaster(Request);
                var data = new { Status = 1, msg = "Record edit Successfully." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        // ================= Json Results ===================
        [HttpPost, ActionName("WardDT")]
        public JsonResult WardMasterDataTable()
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
                repoWardMaster _repoWardMaster = new repoWardMaster();
                var _data = _repoWardMaster.WardDT(searchVal, null, skip, length, sortBy, columnIndex);
                var data = new { Status = 1, data = _data, recordsTotal = repoWardMaster.totalrows, recordsFiltered = repoWardMaster.totalrows };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, data = "", msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [authenticationfilter]
        [HttpGet]
        public JsonResult Getdataid()
        {
            try
            {
                int primaryId = int.Parse(Request.QueryString["primaryId"]);
                var _data = new repoWardMaster().WardDT("", primaryId, 0, 1, "asc", 0);
                var data = new { data = _data, Status = 1, msg = "Record fetched." };
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