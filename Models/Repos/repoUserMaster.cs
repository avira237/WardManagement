using System;
using IMS;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WM.Models.Repos
{
    public class repoUserMaster
    {
        public static int totalrows;
        public object UserMasterDT(string searchVal, int? primaryId, int skip, int length, string sortby, int columnIndex)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    var data = (from x in context.FUNC_SEARCH_USERMASTER("%" + searchVal + "%", primaryId,null)
                                select x).ToList();
                    if (data.Count > 0)
                    {
                        totalrows = data.Count();
                        if (columnIndex == 1)
                            return (sortby == "asc") ? data.OrderBy(x => x.PrimaryId).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.PrimaryId).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 2)
                            return (sortby == "asc") ? data.OrderBy(x => x.RoleId).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.RoleId).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 3)
                            return (sortby == "asc") ? data.OrderBy(x => x.UserName).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.UserName).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 4)
                            return (sortby == "asc") ? data.OrderBy(x => x.UserId).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.UserId).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 5)
                            return (sortby == "asc") ? data.OrderBy(x => x.Password).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.Password).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 6)
                            return (sortby == "asc") ? data.OrderBy(x => x.Mobile).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.Mobile).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 7)
                            return (sortby == "asc") ? data.OrderBy(x => x.Email).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.Email).Skip(skip).Take(length).ToList();
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
        public bool SaveUserMaster(HttpRequestBase Request)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    USERMASTER _USERMASTER = new USERMASTER();
                    _USERMASTER.USER_ID = Request.Form["USERID"].ToUpper();
                    _USERMASTER.USERNAME = Request.Form["USERNAME"].ToUpper();
                    string encpass = Enc.Encrypt(Request.Form["PASSWORD"]);
                    _USERMASTER.PASSWORD = encpass;
                    _USERMASTER.MOBILE = Int64.Parse(Request.Form["MOBILE"]);
                    _USERMASTER.EMAIL= Request.Form["EMAIL"];
                    _USERMASTER.ROLE_ID = int.Parse(Request.Form["ROLEID"]);
                    _USERMASTER.IS_LOCKED = Convert.ToBoolean(Request.Form["ISLOCKED"]);
                    _USERMASTER.LOCKED_REASON = Request.Form["LOCKEDREASON"].ToUpper();                    
                    context.USERMASTERs.Add(_USERMASTER);
                    if (context.SaveChanges() > 0)
                    {
                        return true;
                    }
                    throw new Exception("Failed to save user Data.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool EdituserMaster(HttpRequestBase Request)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    int primaryId = int.Parse(Request.Form["ID"]);
                    var data = (from x in context.USERMASTERs
                                where x.ID == primaryId
                                select x).SingleOrDefault();
                    if (data != null)
                    {
                        data.USERNAME = Request.Form["USERNAME"].ToUpper();
                        data.USER_ID = Request.Form["USERID"].ToUpper();
                        string encpass = Enc.Encrypt(Request.Form["PASSWORD"]);
                        data.PASSWORD = encpass;
                        data.MOBILE = Int64.Parse(Request.Form["MOBILE"]);
                        data.EMAIL = Request.Form["EMAIL"].ToUpper();
                        data.ROLE_ID = int.Parse(Request.Form["ROLEID"]);
                        data.IS_LOCKED = Convert.ToBoolean(Request.Form["ISLOCKED"]);
                        data.LOCKED_REASON = Request.Form["LOCKEDREASON"].ToUpper();
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

        public object getrole(int ID)
        {
            try
            {
                WMEntities context = new WMEntities();
                context.Configuration.LazyLoadingEnabled = false;
                var _data = (from x in context.FUNC_SEARCH_USERMASTER("%%", null, ID)
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

        public bool getuserid(string id)
        {
            try
            {
                WMEntities context = new WMEntities();
                context.Configuration.LazyLoadingEnabled = false;
                var _data = (from x in context.USERMASTERs
                             where x.USER_ID == id
                             select new
                             {
                                 x.USER_ID
                             }).SingleOrDefault();
                if (_data != null)
                {
                    return false;
                }
                else
                {
                    return true;
                    throw new Exception("No record found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}