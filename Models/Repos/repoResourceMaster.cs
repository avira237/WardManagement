using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WM.Models;
namespace WM.Models.Repos
{
    public class repoResourceMaster
    {
        public static int totalrows;
        public object resourceDT(string searchVal, int? primaryId, int skip, int length, string sortby, int columnIndex)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    var data = (from x in context.RESOURCEMASTERs
                                select new
                                {
                                    PrimaryId = x.ID,
                                    ResourceName = x.RESOURCE_NAME
                                }).ToList();

                    if (data.Count > 0)
                    {
                        totalrows = data.Count();
                        if (columnIndex == 0)
                            return (sortby == "asc") ? data.OrderBy(x => x.PrimaryId).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.PrimaryId).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 1)
                            return (sortby == "asc") ? data.OrderBy(x => x.ResourceName).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.ResourceName).Skip(skip).Take(length).ToList();
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

        public bool editresourceMaster(HttpRequestBase Request)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    int primaryId = int.Parse(Request.Form["ID"]);
                    var data = (from x in context.RESOURCEMASTERs
                                where x.ID == primaryId
                                select x).SingleOrDefault();
                    if (data != null)
                    {
                        data.RESOURCE_NAME = Request.Form["Resourcename"].ToUpper();
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

        public object SaveResourceMaster(HttpRequestBase Request,string Resourse)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    return (context.PROC_CREATE_RESOURCE(Resourse));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}