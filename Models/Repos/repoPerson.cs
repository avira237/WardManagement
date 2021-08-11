using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace WM.Models.Repos
{
    public class repoPerson
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
        public object getBooth(int ID)
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
                return _data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public object getSociety(int ID)
        {
            try
            {
                WMEntities context = new WMEntities();
                context.Configuration.LazyLoadingEnabled = false;
                var _data = (from x in context.SOCIETYMASTERs
                             where x.BOOTH_ID == ID
                             join y in context.BOOTHMASTERs
                             on x.BOOTH_ID equals y.ID
                             select new
                             {
                                 x.ID,
                                 x.SOCIETYNAME

                             }
                       ).ToList();
                return _data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditPersonMaster(HttpRequestBase Request)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    int primaryId = int.Parse(Request.Form["ID"]);
                    var data = (from x in context.PERSONINFOes
                                where x.ID == primaryId
                                select x).SingleOrDefault();
                    if (data != null)
                    {
                        data.UNIQUE_NUMBER = Request.Form["UNIQUENUMBER"];
                        data.VOTER_ID = Request.Form["VOTERID"];
                        data.NAME = Request.Form["NAME"].ToUpper();
                        data.GENDER = Request.Form["GENDER"].ToUpper();
                        data.DOB = DateTime.Parse(Request.Form["DOB"]);
                        data.AGE = int.Parse(Request.Form["AGE"]);
                        data.ADDRESS = Request.Form["ADDRESS"].ToUpper();
                        data.WARD_ID = int.Parse(Request.Form["WARDID"]);
                        data.MATDAN_ID = int.Parse(Request.Form["MATDANID"]);
                        data.BOOTH_ID = int.Parse(Request.Form["BOOTHID"]);
                        data.SOCIETY_ID = int.Parse(Request.Form["SOCIETYID"]);                       
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

        public object PersonDT(string searchVal, int? primaryId, int skip, int length, string sortby, int columnIndex)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    var data = (from x in context.FUNC_SEARCH_PERSONINFO("%" + searchVal + "%", primaryId)
                                select x).ToList();
                    if (data.Count > 0)
                    {
                        totalrows = data.Count();
                        if (columnIndex == 1)
                            return (sortby == "asc") ? data.OrderBy(x => x.UniqueNumber).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.UniqueNumber).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 2)
                            return (sortby == "asc") ? data.OrderBy(x => x.VoterId).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.VoterId).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 3)
                            return (sortby == "asc") ? data.OrderBy(x => x.PersonName).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.PersonName).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 4)
                            return (sortby == "asc") ? data.OrderBy(x => x.Gender).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.Gender).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 5)
                            return (sortby == "asc") ? data.OrderBy(x => x.Dob).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.Dob).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 6)
                            return (sortby == "asc") ? data.OrderBy(x => x.Age).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.Age).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 7)
                            return (sortby == "asc") ? data.OrderBy(x => x.SocietyName).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.SocietyName).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 8)
                            return (sortby == "asc") ? data.OrderBy(x => x.Address).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.Address).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 9)
                            return (sortby == "asc") ? data.OrderBy(x => x.WardName).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.WardName).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 10)
                            return (sortby == "asc") ? data.OrderBy(x => x.MatdanName).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.MatdanName).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 11)
                            return (sortby == "asc") ? data.OrderBy(x => x.BoothId).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.BoothId).Skip(skip).Take(length).ToList();
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
        public bool SavePerson(HttpRequestBase Request)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    PERSONINFO _PERSONINFO = new PERSONINFO();
                    _PERSONINFO.UNIQUE_NUMBER = Request.Form["UNIQUENUMBER"];
                    _PERSONINFO.VOTER_ID = Request.Form["VOTERID"];
                    _PERSONINFO.NAME = Request.Form["NAME"].ToUpper();
                    _PERSONINFO.GENDER = Request.Form["GENDER"].ToUpper();
                    _PERSONINFO.DOB = DateTime.Parse(Request.Form["DOB"]);
                    _PERSONINFO.AGE = int.Parse(Request.Form["AGE"]);
                    _PERSONINFO.ADDRESS = Request.Form["ADDRESS"].ToUpper();
                    _PERSONINFO.WARD_ID = int.Parse(Request.Form["WARDID"]);
                    _PERSONINFO.MATDAN_ID = int.Parse(Request.Form["MATDANID"]);
                    _PERSONINFO.BOOTH_ID = int.Parse(Request.Form["BOOTHID"]);
                    _PERSONINFO.SOCIETY_ID = int.Parse(Request.Form["SOCIETYID"]);
                    _PERSONINFO.ISVOTED = false;
                    context.PERSONINFOes.Add(_PERSONINFO);
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
    }
}