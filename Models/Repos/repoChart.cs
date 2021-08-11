using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WM.Models.Repos
{
    public class repoChart
    {
        public object chartmatdanMathak(int id)
        {
            using (WMEntities context = new WMEntities())
            {
                var data = (from S in context.BOOTHMASTERs
                            join P in context.PERSONINFOes
                            on S.ID equals P.BOOTH_ID
                            where
                              S.MATDAN_ID == id
                            group new { S, P } by new
                            {
                                S.BOOTH1
                            } into g
                            orderby
                              g.Key.BOOTH1
                            select new
                            {
                                boothno = g.Key.BOOTH1,
                                Voted = g.Sum(p => (p.P.ISVOTED == true ? 1 : 0)),
                                NotVoted = g.Sum(p => (p.P.ISVOTED == false ? 1 : 0)),
                                Total = g.Count(p => p.P.ISVOTED != null)
                            }).ToList();
                List<int?> booth = new List<int?>();
                List<int> voted = new List<int>();
                List<int> notVoted = new List<int>();
                List<int> total = new List<int>();
                foreach (var item in data)
                {
                    booth.Add(item.boothno);
                    voted.Add(item.Voted);
                    notVoted.Add(item.NotVoted);
                    total.Add(item.Total);
                }
                var _data = new
                {
                    Label = booth,
                    Voted = voted,
                    NotVoted = notVoted,
                    Total = total
                };
                return _data;
            }
        }

        public object chartWard(int id)
        {
            using (WMEntities context = new WMEntities())
            {
                var data = (from S in context.MATDANMATHAKMASTERs
                            join P in context.PERSONINFOes
                            on S.ID equals P.MATDAN_ID
                            where
                              S.WARD_ID == id
                            group new { S, P } by new
                            {
                                S.NAME
                            } into g
                            orderby
                              g.Key.NAME
                            select new
                            {
                                matName = g.Key.NAME,
                                Voted = g.Sum(p => (p.P.ISVOTED == true ? 1 : 0)),
                                NotVoted = g.Sum(p => (p.P.ISVOTED == false ? 1 : 0)),
                                Total = g.Count(p => p.P.ISVOTED != null)
                            }).ToList();
                List<string> matdan = new List<string>();
                List<int> voted = new List<int>();
                List<int> notVoted = new List<int>();
                List<int> total = new List<int>();
                foreach (var item in data)
                {
                    matdan.Add(item.matName);
                    voted.Add(item.Voted);
                    notVoted.Add(item.NotVoted);
                    total.Add(item.Total);
                }
                var _data = new
                {
                    Label = matdan,
                    Voted = voted,
                    NotVoted = notVoted,
                    Total = total
                };
                return _data;
            }
        }
      
        public object chartbooth(int id)
        {
            using (WMEntities context = new WMEntities())
            {
                var data = (from S in context.SOCIETYMASTERs
                            join P in context.PERSONINFOes
                            on S.ID equals P.SOCIETY_ID
                            where
                              S.BOOTH_ID == id
                            group new { S, P } by new
                            {
                                S.SOCIETYNAME
                            } into g
                            orderby
                              g.Key.SOCIETYNAME
                            select new
                            {
                                SocName = g.Key.SOCIETYNAME,
                                Voted = g.Sum(p => (p.P.ISVOTED == true ? 1 : 0)),
                                NotVoted = g.Sum(p => (p.P.ISVOTED == false ? 1 : 0)),
                                Total = g.Count(p => p.P.ISVOTED != null)
                            }).ToList();
                List<string> Societies = new List<string>();
                List<int> voted = new List<int>();
                List<int> notVoted = new List<int>();
                List<int> total = new List<int>();
                foreach (var item in data)
                {
                    Societies.Add(item.SocName);
                    voted.Add(item.Voted);
                    notVoted.Add(item.NotVoted);
                    total.Add(item.Total);
                }
                var _data = new
                {
                    Label = Societies,
                    Voted = voted,
                    NotVoted = notVoted,
                    Total = total
                };
                return _data;
            }
        }
        public object chartsociety(int id)
        {
            using (WMEntities context = new WMEntities())
            {
                var data = (from S in context.SOCIETYMASTERs
                            join P in context.PERSONINFOes
                            on S.ID equals P.SOCIETY_ID
                            where
                              S.ID == id
                            group new { S, P } by new
                            {
                                S.SOCIETYNAME
                            } into g
                            orderby
                              g.Key.SOCIETYNAME
                            select new
                            {
                                SocName = g.Key.SOCIETYNAME,
                                Voted = g.Sum(p => (p.P.ISVOTED == true ? 1 : 0)),
                                NotVoted = g.Sum(p => (p.P.ISVOTED == false ? 1 : 0)),
                                Total = g.Count(p => p.P.ISVOTED != null)
                            }).ToList();
                List<string> Societies = new List<string>();
                List<int> voted = new List<int>();
                List<int> notVoted = new List<int>();
                List<int> total = new List<int>();
                foreach (var item in data)
                {
                    Societies.Add(item.SocName);
                    voted.Add(item.Voted);
                    notVoted.Add(item.NotVoted);
                    total.Add(item.Total);
                }
                var _data = new
                {
                    Label = Societies,
                    Voted = voted,
                    NotVoted = notVoted,
                    Total = total
                };
                return _data;
            }
        }
    }
}