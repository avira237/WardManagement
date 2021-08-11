using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using WM.Models;

namespace WM.Models.Repos
{
    public class repoSociety
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
        public object getbooth(int ID)
        {
            try
            {
                WMEntities context = new WMEntities();
                context.Configuration.LazyLoadingEnabled = false;
                var _data = (from x in context.BOOTHMASTERs
                             where x.MATDAN_ID == ID
                             join y in context.MATDANMATHAKMASTERs
                             on x.MATDAN_ID equals y.ID
                             select new
                             {
                                 x.ID,
                                 x.BOOTH1
                             }
                    ).ToList();
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
        public bool SaveSociety(HttpRequestBase Request)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    SOCIETYMASTER _SOCIETYMASTER = new SOCIETYMASTER();
                    _SOCIETYMASTER.SOCIETYNAME = Request.Form["SOCIETYNAME"].ToUpper();
                    _SOCIETYMASTER.LANDMARK = Request.Form["LANDMARK"].ToUpper();
                    _SOCIETYMASTER.WARD_ID = int.Parse(Request.Form["WARDID"]);
                    _SOCIETYMASTER.MATDAN_ID = int.Parse(Request.Form["MATDANID"]);
                    _SOCIETYMASTER.BOOTH_ID = int.Parse(Request.Form["BOOTHID"]);
                    context.SOCIETYMASTERs.Add(_SOCIETYMASTER);
                    if (context.SaveChanges() > 0)
                    {
                        return true;
                    }
                    throw new Exception("Failed to save SocietyMaster Data.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object SocietyDT(string searchVal, int? primaryId, int skip, int length, string sortby, int columnIndex)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    var data = (from x in context.FUNC_SEARCH_SOCIETY("%" + searchVal + "%", primaryId)
                                select x).ToList();
                    if (data.Count > 0)
                    {
                        totalrows = data.Count();
                        if (columnIndex == 1)
                            return (sortby == "asc") ? data.OrderBy(x => x.PrimaryId).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.PrimaryId).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 2)
                            return (sortby == "asc") ? data.OrderBy(x => x.SocietyName).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.SocietyName).Skip(skip).Take(length).ToList();
                        if (columnIndex == 3)
                            return (sortby == "asc") ? data.OrderBy(x => x.WardName).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.WardName).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 4)
                            return (sortby == "asc") ? data.OrderBy(x => x.MardanName).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.MardanName).Skip(skip).Take(length).ToList();
                        if (columnIndex == 5)
                            return (sortby == "asc") ? data.OrderBy(x => x.Booth1).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.Booth1).Skip(skip).Take(length).ToList();
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

        public bool EditSocietyMaster(HttpRequestBase Request)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    int primaryId = int.Parse(Request.Form["ID"]);
                    var data = (from x in context.SOCIETYMASTERs
                                where x.ID == primaryId
                                select x).SingleOrDefault();
                    if (data != null)
                    {
                        data.SOCIETYNAME = Request.Form["SOCIETYNAME"].ToUpper();
                        data.LANDMARK = Request.Form["LANDMARK"].ToUpper();
                        data.WARD_ID = int.Parse(Request.Form["WARDID"]);
                        data.MATDAN_ID = int.Parse(Request.Form["MATDANID"]);
                        data.BOOTH_ID = int.Parse(Request.Form["BOOTHID"]);
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