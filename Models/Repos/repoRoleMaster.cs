using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace WM.Models.Repos
{
    public class repoRoleMaster
    {
        public static int totalrows;
        public object RightDT(int id, string searchVal, int? primaryId, int skip, int length, string sortby, int columnIndex)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    int PrimaryId = id;
                    var data = (from x in context.RIGHTMASTERs
                                where x.ROLE_ID== PrimaryId
                                join y in context.RESOURCEMASTERs
                                on x.RESOURCE_ID equals y.ID
                                select new
                                {
                                    y.RESOURCE_NAME,
                                    x.ID,
                                    x.ISADD,
                                    x.ISUPDATE,
                                    x.ISVIEW,
                                    x.ISPRINT,
                                    x.ISDELETE
                                }).ToList();
                    if (data.Count > 0)
                    {
                        totalrows = data.Count();
                        if (columnIndex == 0)
                            return (sortby == "asc") ? data.OrderBy(x => x.RESOURCE_NAME).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.RESOURCE_NAME).Skip(skip).Take(length).ToList();
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

        public bool Editaddcheckbox(Boolean check, int id)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    int primaryId = id;
                    var data = (from x in context.RIGHTMASTERs
                                where x.ID == primaryId
                                select x).SingleOrDefault();
                    if (data != null)
                    {
                        data.ISADD = check;
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
        public bool Editupdatecheckbox(Boolean check, int id)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    int primaryId = id;
                    var data = (from x in context.RIGHTMASTERs
                                where x.ID == primaryId
                                select x).SingleOrDefault();
                    if (data != null)
                    {
                        data.ISUPDATE = check;
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

        public bool Editviewcheckbox(Boolean check, int id)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    int primaryId = id;
                    var data = (from x in context.RIGHTMASTERs
                                where x.ID == primaryId
                                select x).SingleOrDefault();
                    if (data != null)
                    {
                        data.ISVIEW = check;
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

        public bool Editprintcheckbox(Boolean check, int id)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    int primaryId = id;
                    var data = (from x in context.RIGHTMASTERs
                                where x.ID == primaryId
                                select x).SingleOrDefault();
                    if (data != null)
                    {
                        data.ISPRINT = check;
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

        public bool Editdeletecheckbox(Boolean check, int id)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    int primaryId = id;
                    var data = (from x in context.RIGHTMASTERs
                                where x.ID == primaryId
                                select x).SingleOrDefault();
                    if (data != null)
                    {
                        data.ISDELETE = check;
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