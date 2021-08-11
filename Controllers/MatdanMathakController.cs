using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WM.Models;
using WM.Models.Repos;
using WM.Filter;
namespace WM.Controllers
{
    public class MatdanMathakController : Controller
    {
        // GET: MatdanMathak
        [authenticationfilter]
        public ActionResult Index()
        {
            return View("Index");
        }
        [authenticationfilter]
        [HttpGet]
        public JsonResult Getdataid()
        {
            try
            {
                int primaryId = int.Parse(Request.QueryString["primaryId"]);
                var _data = new repoMatdanMathak().MatdanMathaDT("", primaryId, 0, 1, "asc", 0);
                var data = new { data = _data, Status = 1, msg = "Record fetched." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [authenticationfilter]
        [HttpPost, ActionName("editMatdanMaster")]
        public JsonResult editMatdanMathakMasterDetails()
        {
            try
            {
                new repoMatdanMathak().editMatdanMathakMaster(Request);
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
        [HttpPost, ActionName("SaveMatdanMathakMaster")]
        public JsonResult SavematdanMathakMasterDetails()
        {
            try
            {
                new repoMatdanMathak().SaveMatdanMathakMaster(Request);
                var data = new { Status = 1, msg = "Record Saved Successfully." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult getDepartment()
        {
            WMEntities context = new WMEntities();
            return Json(context.WARDMASTERs.Select(x => new
            {
                ID = x.ID,
                WARDNAME = x.WARDNAME
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("MatDT")]
        public JsonResult MatdanDataTable()
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

                repoMatdanMathak _repoMatdanMathak = new repoMatdanMathak();
                var _data = _repoMatdanMathak.MatdanMathaDT(searchVal, null, skip, length, sortBy, columnIndex);

                var data = new { Status = 1, data = _data, recordsTotal = repoMatdanMathak.totalrows, recordsFiltered = repoMatdanMathak.totalrows };
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