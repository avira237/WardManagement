using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace WM.Models.Repos
{
    public class repoResource
    {
           public static int totalrows;
        public object ResourceDT(string searchVal, int? primaryId, int skip, int length, string sortby, int columnIndex)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    var data = (from x in context.RESOURCEMASTERs
                                select new
                                {
                                    primaryId=x.ID,
                                    ResourceName=x.RESOURCE_NAME 
                                }).ToList();
                    if (data.Count > 0)
                    {

                        totalrows = data.Count();
                       
                         if (columnIndex == 1)
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
       
    }
}