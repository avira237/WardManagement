using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using WM.Models;

namespace WM.Models.Repos
{
    public class repoMatdanMathak
    { 
        public static int totalrows;
        public object MatdanMathaDT(string searchVal, int? primaryId, int skip, int length, string sortby, int columnIndex)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    var data = (from x in context.FUNC_SEARCH_MATDANMATHAK("%" + searchVal + "%", primaryId, null)
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
        public bool editMatdanMathakMaster(HttpRequestBase Request)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    int primaryId = int.Parse(Request.Form["ID"]);
                    var data = (from x in context.MATDANMATHAKMASTERs
                                where x.ID == primaryId
                                select x).SingleOrDefault();
                    if (data != null)
                    {
                        data.WARD_ID = int.Parse(Request.Form["WARDID"]);
                        data.NAME = Request.Form["NAME"].ToUpper();
                        data.ADDRESS = Request.Form["ADDRESS"].ToUpper();
                        data.MOBILE = Int64.Parse(Request.Form["MOBILE"]);
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
        public bool SaveMatdanMathakMaster(HttpRequestBase Request)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    MATDANMATHAKMASTER _MATDANMATHAK = new MATDANMATHAKMASTER();
                    _MATDANMATHAK.WARD_ID = int.Parse(Request.Form["WARDID"]);
                    _MATDANMATHAK.NAME = Request.Form["NAME"].ToUpper();
                    _MATDANMATHAK.ADDRESS = Request.Form["ADDRESS"].ToUpper();
                    _MATDANMATHAK.MOBILE = Int64.Parse(Request.Form["MOBILE"]);
                    _MATDANMATHAK.CREATED_BY = Convert.ToInt32(HttpContext.Current.Session["Uid"]);
                    _MATDANMATHAK.CREATED_ON = DateTime.Now;
                    context.MATDANMATHAKMASTERs.Add(_MATDANMATHAK);
                    if (context.SaveChanges() > 0)
                    {
                        return true;
                    }
                    throw new Exception("Failed to save MatdanmathakMaster Data.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}