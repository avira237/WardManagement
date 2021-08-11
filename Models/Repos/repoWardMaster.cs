using System;
using System.Linq;
using WM.Models;
using System.Data.Entity;
using System.Web;

namespace WM.Models.Repos
{
    public class repoWardMaster
    {
        public static int totalrows;
        public bool SaveWardMaster(HttpRequestBase Request)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    WARDMASTER _WARDMASTER = new WARDMASTER();
                    _WARDMASTER.WARDNAME = Request.Form["Wardname"].ToUpper();
                    _WARDMASTER.CREATED_BY =Convert.ToInt32(HttpContext.Current.Session["Uid"]);
                    _WARDMASTER.CREATED_ON = DateTime.Now;
                    context.WARDMASTERs.Add(_WARDMASTER);
                    if (context.SaveChanges() > 0)
                    {
                        return true;
                    }
                    throw new Exception("Failed to save WardMaster Data.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool editWardMaster(HttpRequestBase Request)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    int primaryId = int.Parse(Request.Form["ID"]);
                    var data = (from x in context.WARDMASTERs
                                where x.ID == primaryId
                                select x).SingleOrDefault();
                    if (data != null)
                    {
                        data.WARDNAME = Request.Form["Wardname"].ToUpper();
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
        public object WardDT(string searchVal, int? primaryId, int skip, int length, string sortby, int columnIndex)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    var data = (from x in context.FUNC_SEARCH_WARD("%" + searchVal + "%", primaryId)
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
    }
}