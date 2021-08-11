using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using WM.Models;
namespace WM.Filter
{
    public class authenticationfilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["userID"])))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else if (!checkRight(filterContext))
            {

                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                     { "controller", "Login" },
                     { "action", "AccessDenied" }
                });
            }
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {

                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                     { "controller", "Login" },
                     { "action", "Index" }
                });
            }
        }

        public bool checkRight(AuthenticationContext filterContext)
        {
            int RoleId = int.Parse(HttpContext.Current.Session["roleID"].ToString());
            string ResourceName = filterContext.Controller.ValueProvider.GetValue("Resource").AttemptedValue;
            string FileOperation = filterContext.Controller.ValueProvider.GetValue("FileOperation").AttemptedValue;
            using (WMEntities context = new WMEntities())
            {
                var resourceData = (from x in context.RESOURCEMASTERs
                                    where x.RESOURCE_NAME == ResourceName
                                    select new
                                    {
                                        ResourceId = x.ID
                                    }).SingleOrDefault();
                int ResourceId = resourceData.ResourceId;
                var checkRight = (from x in context.RIGHTMASTERs
                                  where x.ROLE_ID == RoleId
                                  where x.RESOURCE_ID == ResourceId
                                  select x).SingleOrDefault();
                switch (FileOperation.ToLower())
                {
                    case "view":
                        return (bool)checkRight.ISVIEW;
                    case "add":
                        return (bool)checkRight.ISADD;
                    case "update":
                        return (bool)checkRight.ISUPDATE;
                    default:
                        return false;
                }

            }
        }
    }
}
