using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WM.Models.Repos
{
    public class repoBoothMaster
    {
        public static int totalrows;
        public object getward()
        {
            try
            {
                WMEntities context = new WMEntities();
                var data = context.WARDMASTERs.Select(x => new
                {
                    ID = x.ID,
                    WARDNAME = x.WARDNAME
                }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public object getmatdanmathak(int ID)
        {
            try
            {
                WMEntities context = new WMEntities();
                context.Configuration.LazyLoadingEnabled = false;
                var _data = (from x in context.FUNC_SEARCH_MATDANMATHAK("%%", null, ID)
                             select x).ToList();
                if (_data.Count > 0)
                {
                    return _data;
                }
                else
                    throw new Exception("No record found.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public object BoothDT(string searchVal, int? primaryId, int skip, int length, string sortby, int columnIndex)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    var data = (from x in context.FUNC_SEARCH_BOOTH("%" + searchVal + "%", primaryId)
                                select x).ToList();
                    if (data.Count > 0)
                    {
                        totalrows = data.Count();
                        if (columnIndex == 1)
                            return (sortby == "asc") ? data.OrderBy(x => x.PrimaryId).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.PrimaryId).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 2)
                            return (sortby == "asc") ? data.OrderBy(x => x.WardName).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.WardName).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 3)
                            return (sortby == "asc") ? data.OrderBy(x => x.MatdanMathakName).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.MatdanMathakName).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 4)
                            return (sortby == "asc") ? data.OrderBy(x => x.Address).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.Address).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 5)
                            return (sortby == "asc") ? data.OrderBy(x => x.Mobile).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.Mobile).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 6)
                            return (sortby == "asc") ? data.OrderBy(x => x.Booth1).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.Booth1).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 7)
                            return (sortby == "asc") ? data.OrderBy(x => x.Booth2).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.Booth2).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 8)
                            return (sortby == "asc") ? data.OrderBy(x => x.Booth3).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.Booth3).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 9)
                            return (sortby == "asc") ? data.OrderBy(x => x.Booth4).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.Booth4).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 10)
                            return (sortby == "asc") ? data.OrderBy(x => x.Booth5).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.Booth5).Skip(skip).Take(length).ToList();
                        else
                            return data;
                    }
                    else
                    {
                        throw new Exception("No records found.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool SaveBooth(HttpRequestBase Request)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    BOOTHMASTER _BOOTHMASTER = new BOOTHMASTER();
                    _BOOTHMASTER.WARD_ID = int.Parse(Request.Form["WARDID"]);
                    _BOOTHMASTER.MATDAN_ID = int.Parse(Request.Form["MATDANID"]);
                    _BOOTHMASTER.BOOTH1 = int.Parse(Request.Form["BoothNo"]);
                    _BOOTHMASTER.BOOTH2 = Request.Form["BoothNo2"] == "" ? (int?)null : int.Parse(Request.Form["BoothNo2"]);
                    _BOOTHMASTER.BOOTH3 = Request.Form["BoothNo3"] == "" ? (int?)null : int.Parse(Request.Form["BoothNo3"]);
                    _BOOTHMASTER.BOOTH4 = Request.Form["BoothNo4"] == "" ? (int?)null : int.Parse(Request.Form["BoothNo4"]);
                    _BOOTHMASTER.BOOTH5 = Request.Form["BoothNo5"] == "" ? (int?)null : int.Parse(Request.Form["BoothNo5"]);
                    _BOOTHMASTER.CREATED_BY = Convert.ToInt32(HttpContext.Current.Session["Uid"]);
                    _BOOTHMASTER.CREATED_ON = DateTime.Now;
                    context.BOOTHMASTERs.Add(_BOOTHMASTER);
                    if (context.SaveChanges() > 0)
                    {
                        return true;
                    }
                    throw new Exception("Failed to save BoothMaster Data.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool EditBoothMaster(HttpRequestBase Request)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    int primaryId = int.Parse(Request.Form["ID"]);
                    var data = (from x in context.BOOTHMASTERs
                                where x.ID == primaryId
                                select x).SingleOrDefault();
                    if (data != null)
                    {
                        data.WARD_ID = int.Parse(Request.Form["WARDID"]);
                        data.MATDAN_ID = int.Parse(Request.Form["MATDANID"]);
                        data.BOOTH1 = int.Parse(Request.Form["BoothNo"]);
                        data.BOOTH2 = Request.Form["BoothNo2"] == "" ? (int?)null : int.Parse(Request.Form["BoothNo2"]);
                        data.BOOTH3 = Request.Form["BoothNo3"] == "" ? (int?)null : int.Parse(Request.Form["BoothNo3"]);
                        data.BOOTH4 = Request.Form["BoothNo4"] == "" ? (int?)null : int.Parse(Request.Form["BoothNo4"]);
                        data.BOOTH5 = Request.Form["BoothNo5"] == "" ? (int?)null : int.Parse(Request.Form["BoothNo5"]);
                        data.UPDATED_BY = Convert.ToInt32(HttpContext.Current.Session["Uid"]);
                        data.UPDATED_ON = DateTime.Now;
                        if (context.SaveChanges() > 0)
                        {
                            return true;
                        }
                        else
                        {
                            throw new Exception("Failed to update record.");
                        }
                    }
                    else
                    {
                        throw new Exception("No such Record found for update.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}