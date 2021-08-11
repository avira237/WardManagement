using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.Models;
using WM.Models.Repos;
using WM.Filter;

namespace WM.Controllers
{
    public class SocietyController : Controller
    {
        // GET: Society
        [authenticationfilter]
        [HttpGet, ActionName("Index")]
        public ActionResult Index()
        {
            return View();
        }
        [authenticationfilter]
        [HttpPost, ActionName("SaveSociety")]
        public JsonResult SaveSocietyMaster()
        {
            try
            {
                new repoSociety().SaveSociety(Request);
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
        [HttpPost, ActionName("editSociety")]
        public JsonResult EditSocietyMaster()
        {
            try
            {
                new repoSociety().EditSocietyMaster(Request);
                var data = new { Status = 1, msg = "Record edit Successfully." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult getwarddata()
        {
            try
            {
                repoSociety _repoSociety = new repoSociety();
                var data = _repoSociety.getward();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost, ActionName("getmatdanmathak")]

        public JsonResult getmatdanmathakdata()
        {
            try
            {
                repoSociety _repoSociety = new repoSociety();
                int WardId = int.Parse(Request.Form["ID"]);
                var _data = _repoSociety.getmatdanmathak(WardId);
                var data = new { data = _data, Status = 1, msg = "Data fetched." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost, ActionName("getbooth")]
        public JsonResult getboothdata()
        {
            try
            {
                repoSociety _repoSociety = new repoSociety();
                int MatdanId = int.Parse(Request.Form["ID"]);
                var _data = _repoSociety.getbooth(MatdanId);
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
        [HttpGet]
        public JsonResult Getdataid()
        {
            try
            {
                int primaryId = int.Parse(Request.QueryString["primaryId"]);
                var _data = new repoSociety().SocietyDT("", primaryId, 0, 1, "asc", 0);
                var data = new { data = _data, Status = 1, msg = "Record fetched." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult editgetmatdanmathakdata()
        {
            try
            {
                repoSociety _repoSociety = new repoSociety();
                int WardId = int.Parse(Request.Form["ID"]);
                var _data = _repoSociety.getmatdanmathak(WardId);
                var data = new { data = _data, Status = 1, msg = "Data fetched." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost, ActionName("societyDT")]
        public JsonResult SocietyDataTable()
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

                repoSociety _repoSocietyMaster = new repoSociety();
                var _data = _repoSocietyMaster.SocietyDT(searchVal, null, skip, length, sortBy, columnIndex);

                var data = new { Status = 1, data = _data, recordsTotal = repoSociety.totalrows, recordsFiltered = repoSociety.totalrows };


                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var data = new { Status = 0, data = "", msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

    }
}