using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicRoom.Models;

namespace MusicRoom.Controllers
{
    public class TagCloudController : Controller
    {
        //
        // GET: /TagCloud/
        [ChildActionOnly]
        [WebApiOutputCache(120, 60, false)]
        public ActionResult Index()
        {
            MusicRoomContext context = new MusicRoomContext();
            return PartialView(context);
        }

    }
}
