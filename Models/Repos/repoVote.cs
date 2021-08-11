using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WM.Models.Repos
{
    public class repoVote
    {
        public static int totalrows;
        public object SearchDT(HttpRequestBase Request)
        {
            try
            {
                var startRecords = Request.Form["start"];
                var lengthRecords = Request.Form["length"];
                int skip = (startRecords != null) ? int.Parse(startRecords) : 0;
                int length = (lengthRecords != null) ? int.Parse(lengthRecords) : 10;
                string searchVal = Request.Form["search[value]"];
                string sortBy = Request.Form["order[0][dir]"];
                int columnIndex = !Request.Form.AllKeys.Contains("order[0][column]") ? 0 : int.Parse(Request.Form["order[0][column]"]);
                using (WMEntities context = new WMEntities())
                {
                    var search = Request.Form["SEARCH"];
                    var ward = Request.Form["WARDID"] == "" ? (int?)null : int.Parse(Request.Form["WARDID"]);
                    var matdan = Request.Form["MATDANID"] == "" ? (int?)null : int.Parse(Request.Form["MATDANID"]);
                    var booth = Request.Form["BOOTHNO"] == "" ? (int?)null : int.Parse(Request.Form["BOOTHNO"]);
                    var society = Request.Form["SOCIETYID"] == "" ? (int?)null : int.Parse(Request.Form["SOCIETYID"]);
                    var data = (from x in context.PROC_GET_VOTING_DATA("%" +search+"%", ward, matdan, booth, society)
                                select x).ToList();
                    if (data.Count > 0)
                    { 
                        totalrows = data.Count();
                         if (columnIndex == 1)
                            return (sortBy == "asc") ? data.OrderBy(x => x.Name).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.Name).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 2)
                            return (sortBy == "asc") ? data.OrderBy(x => x.UniqueId).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.UniqueId).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 3)
                            return (sortBy == "asc") ? data.OrderBy(x => x.VoterId).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.VoterId).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 4)
                            return (sortBy == "asc") ? data.OrderBy(x => x.WardName).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.WardName).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 5)
                            return (sortBy == "asc") ? data.OrderBy(x => x.MatdanName).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.MatdanName).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 6)
                            return (sortBy == "asc") ? data.OrderBy(x => x.Address).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.Address).Skip(skip).Take(length).ToList();
                        else if (columnIndex == 7)
                            return (sortBy == "asc") ? data.OrderBy(x => x.SocietyName).Skip(skip).Take(length).ToList()
                                : data.OrderByDescending(x => x.SocietyName).Skip(skip).Take(length).ToList();
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

        public bool EditVote(Boolean vote, int id)
        {
            try
            {
                using (WMEntities context = new WMEntities())
                {
                    int primaryId = id;
                    var data = (from x in context.PERSONINFOes
                                where x.ID == primaryId
                                select x).SingleOrDefault();
                    if (data != null)
                    {
                        data.ISVOTED = vote;
                        data.VOTE_TIMESTAMP = DateTime.Now;
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