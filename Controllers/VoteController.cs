using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.Models.Repos;
using WM.Models;
using WM.Filter;

namespace WM.Controllers
{
    public class VoteController : Controller
    {
        // GET: Vote
        [authenticationfilter]
        [HttpGet, ActionName("Index")]
        public ActionResult Index()
        {
            return View("Index");
        }
        [authenticationfilter]
        [HttpPost, ActionName("VoteChange")]
        public ActionResult VoteChngedata()
        {
            try
            {
                var vote = true;
                int primaryId = int.Parse(Request.QueryString["primaryId"]);
                new repoVote().EditVote(vote, primaryId);
                var data = new { Status = 1, msg = "Voted" };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var data = new { Status = 0, msg = ex.Message };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult getWard()
        {
            WMEntities context = new WMEntities();
            return Json(context.WARDMASTERs.Select(x => new
            {
                ID = x.ID,
                WARDNAME = x.WARDNAME
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult getMatdanMathak()
        {
            WMEntities context = new WMEntities();
            return Json(context.MATDANMATHAKMASTERs.Select(x => new
            {
                ID = x.ID,
                NAME = x.NAME
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult getSociety()
        {
            WMEntities context = new WMEntities();
            return Json(context.SOCIETYMASTERs.Select(x => new
            {
                ID = x.ID,
                SOCIETYNAME = x.SOCIETYNAME
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        [authenticationfilter]
        [HttpPost, ActionName("VoteDT")]
        public JsonResult VoteDataTable()
        {
            try
            {
                repoVote _repoVote = new repoVote();
                var _data = _repoVote.SearchDT(Request);
                var data = new { Status = 1, data = _data, recordsTotal = repoVote.totalrows, recordsFiltered = repoVote.totalrows };
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