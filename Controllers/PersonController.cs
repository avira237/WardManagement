using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.Models.Repos;
using WM.Filter;

namespace WM.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
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
                repoPerson _repoPerson = new repoPerson();
                var data = _repoPerson.getward();
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
                repoPerson _repoPerson = new repoPerson();
                int WardId = int.Parse(Request.Form["ID"]);
                var _data = _repoPerson.getmatdanmathak(WardId);
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
                repoPerson _repoPerson = new repoPerson();
                int MatdanId = int.Parse(Request.Form["ID"]);
                var _data = _repoPerson.getBooth(MatdanId);
                var data = new { data = _data, Status = 1, msg = "Data fetched." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost, ActionName("getSociety")]

        public JsonResult getSocietydata()
        {
            try
            {
                repoPerson _repoPerson = new repoPerson();
                int BoothId = int.Parse(Request.Form["ID"]);
                var _data = _repoPerson.getSociety(BoothId);
                var data = new { data = _data, Status = 1, msg = "Data fetched." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost, ActionName("PersonDT")]
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

                repoPerson _repoPerson = new repoPerson();
                var _data = _repoPerson.PersonDT(searchVal, null, skip, length, sortBy, columnIndex);

                var data = new { Status = 1, data = _data, recordsTotal = repoPerson.totalrows, recordsFiltered = repoPerson.totalrows };


                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var data = new { Status = 0, data = "", msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [authenticationfilter]
        [HttpPost, ActionName("SavePerson")]
        public JsonResult SavePersonMaster()
        {
            try
            {
                new repoPerson().SavePerson(Request);
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
        [HttpPost, ActionName("editPerson")]
        public JsonResult editPersonMaster()
        {
            try
            {
                new repoPerson().EditPersonMaster(Request);
                var data = new { Status = 1, msg = "Record edit Successfully." };
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
                repoPerson _repoPerson = new repoPerson();
                int WardId = int.Parse(Request.Form["ID"]);
                var _data = _repoPerson.getmatdanmathak(WardId);
                var data = new { data = _data, Status = 1, msg = "Data fetched." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult editboothdata()
        {
            try
            {
                repoPerson _repoPerson = new repoPerson();
                int MatdanId = int.Parse(Request.Form["ID"]);
                var _data = _repoPerson.getBooth(MatdanId);
                var data = new { data = _data, Status = 1, msg = "Data fetched." };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult editsocietydata()
        {
            try
            {
                repoPerson _repoPerson = new repoPerson();
                int BoothId = int.Parse(Request.Form["ID"]);
                var _data = _repoPerson.getSociety(BoothId);
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
                var _data = new repoPerson().PersonDT("", primaryId, 0, 1, "asc", 0);
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