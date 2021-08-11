using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.Models;
using WM.Models.Repos;
namespace WM.Controllers
{
    public class ResorceController : Controller
    {
        // GET: Resorce
        [HttpGet, ActionName("Index")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost, ActionName("ResourceDT")]
        public JsonResult ResourceMasterDataTable()
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

                repoResource _repoResource = new repoResource();
                var _data = _repoResource.ResourceDT(searchVal, null, skip, length, sortBy, columnIndex);

                var data = new { Status = 1, data = _data, recordsTotal = repoResource.totalrows, recordsFiltered = repoResource.totalrows };


                return Json(data, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult Getdataid()
        {
            try
            {
                using (WMEntities context = new WMEntities()) {
                int primaryId = int.Parse(Request.QueryString["primaryId"]);
                var _data = (from x in context.RESOURCEMASTERs
                            where x.ID==primaryId
                            select new
                            {
                                ResourceName =x.RESOURCE_NAME
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
    }
}