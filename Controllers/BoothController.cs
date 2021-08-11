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
    public class BoothController : Controller
    {
        // GET: Booth
        [authenticationfilter]
        [HttpGet, ActionName("Index")]

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getwarddata()
        {
            try
            {
                repoBoothMaster _repoBooth = new repoBoothMaster();
                var data = _repoBooth.getward();
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
                repoBoothMaster _repoBooth = new repoBoothMaster();
                int WardId = int.Parse(Request.Form["ID"]);
                var _data = _repoBooth.getmatdanmathak(WardId);
                var data = new { data = _data, Status = 1, msg = "Data fetched." };
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
                repoBoothMaster _repoBooth = new repoBoothMaster();
                int WardId = int.Parse(Request.Form["ID"]);
                var _data = _repoBooth.getmatdanmathak(WardId);
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
        [HttpPost, ActionName("SaveBooth")]
        public JsonResult SaveBoothMaster()
        {
            try
            {
                new repoBoothMaster().SaveBooth(Request);
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
        [HttpPost, ActionName("editBooth")]
        public JsonResult EditBoothMaster()
        {
            try
            {
                new repoBoothMaster().EditBoothMaster(Request);
                var data = new { Status = 1, msg = "Record edit Successfully." };
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
                var _data = new repoBoothMaster().BoothDT("", primaryId, 0, 1, "asc", 0);
                var data = new { data = _data, Status = 1, msg = "Record fetched." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost, ActionName("BoothDT")]
        public JsonResult BoothDataTable()
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

                repoBoothMaster _repoBoothMaster = new repoBoothMaster();
                var _data = _repoBoothMaster.BoothDT(searchVal, null, skip, length, sortBy, columnIndex);

                var data = new { Status = 1, data = _data, recordsTotal = repoBoothMaster.totalrows, recordsFiltered = repoBoothMaster.totalrows };


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