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
    public class ResourceController : Controller
    {
        // GET: Resource
        [authenticationfilter]
        [HttpGet, ActionName("Index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("resourceDT")]
        public JsonResult resourceDataTable()
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

                repoResourceMaster _repoResourceMaster = new repoResourceMaster();
                var _data = _repoResourceMaster.resourceDT(searchVal, null, skip, length, sortBy, columnIndex);

                var data = new { Status = 1, data = _data, recordsTotal = repoResourceMaster.totalrows, recordsFiltered = repoResourceMaster.totalrows };


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
                using (WMEntities context = new WMEntities())
                {
                    int primaryId = int.Parse(Request.QueryString["primaryId"]);
                    var _data = (from x in context.RESOURCEMASTERs
                                 where x.ID == primaryId
                                 select new
                                 {
                                     primaryId = x.ID,
                                     ResourceName = x.RESOURCE_NAME
                                 }).ToList();
                    var data = new
                    {
                        Status = 1,
                        data = _data
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [authenticationfilter]
        [HttpPost, ActionName("editresourceMaster")]
        public JsonResult editresourceMasterDetails()
        {
            try
            {
                new repoResourceMaster().editresourceMaster(Request);
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
        [HttpPost, ActionName("SaveResourceMaster")]
        public JsonResult SaveResourceMasterDetails()
        {
            try
            {
                var Resourse = Request.Form["Resourcename"];
                new repoResourceMaster().SaveResourceMaster(Request, Resourse);
                var data = new { Status = 1, msg = "Record Saved Successfully." };
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