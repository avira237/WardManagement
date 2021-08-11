
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
    public class ChartController : Controller
    {
        [authenticationfilter]
        // GET: Chart
        public ActionResult Index()
        {

            return View();
        }

        public JsonResult chartMatdanMathak()
        {
            try
            {
                int primaryId = int.Parse(Request.QueryString["matdanid"]);
                var _data = new repoChart().chartmatdanMathak(primaryId);

                var data = new { data = _data, Status = 1 };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult chartward()
        {
            try
            {
                int primaryId = int.Parse(Request.QueryString["wardid"]);
                var _data = new repoChart().chartWard(primaryId);

                var data = new { data = _data, Status = 1 };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult chartBooth()
        {
            try
            {
                int primaryId = int.Parse(Request.QueryString["boothid"]);
                var _data = new repoChart().chartbooth(primaryId);

                var data = new { data = _data, Status = 1 };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult chartsociety()
        {
            try
            {
                int primaryId = int.Parse(Request.QueryString["societyid"]);
                var _data = new repoChart().chartsociety(primaryId);

                var data = new { data = _data, Status = 1 };
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

