using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MusicRoom.Filters;
using MusicRoom.Models;
using MusicRoom.Repository;
using PagedList;

namespace MusicRoom.Controllers
{

    public class HomeController : Controller
    {

        private static bool searchIndexCreated;

        [InitializeSimpleMembership]
        public ViewResult Index(int? page)
        {
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            if (searchIndexCreated != true)
                CreateIndex();
            TrackRepository track = new TrackRepository();

            return View(track.All.OrderByDescending(rating => rating.Like).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }

        public ActionResult ChangeTheme(string theme, string returnUrl)
        {
            HttpCookie aCookie = new HttpCookie("themeName");
            aCookie.Value = theme;
            aCookie.Expires = DateTime.Now.AddDays(14);
            Response.Cookies.Add(aCookie);
            return Redirect(returnUrl);
        }

        public void CreateIndex()
        {
            GoLucene.ClearLuceneIndex();
            GoLucene.AddUpdateLuceneIndex(TrackDataRepository.GetAll());
            searchIndexCreated = true;
        }

        public ActionResult Autocomplete(string term)
        {
            var tags = GoLucene.Search(term, "Tags").ToArray();
            StringBuilder tagNames = new StringBuilder();
            foreach (var tag in tags)
            {
                tagNames.Append(tag.Tags + ",");
            }
            string[] TagList = tagNames.ToString().Split(',');
            return Json(TagList, JsonRequestBehavior.AllowGet);
        }

    }
}
