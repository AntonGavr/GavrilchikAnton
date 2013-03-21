using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicRoom.Models;
using WebMatrix.WebData;

namespace MusicRoom.Controllers
{   
    public class PlayListsController : Controller
    {
		private readonly IPlayListRepository playlistRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public PlayListsController() : this(new PlayListRepository())
        {
        }

        public PlayListsController(IPlayListRepository playlistRepository)
        {
			this.playlistRepository = playlistRepository;
        }

        //
        // GET: /PlayLists/
         [WebApiOutputCache(120, 60, false)]
        public ViewResult Index()
        {
            return View(playlistRepository.All);
        }

        //
        // GET: /PlayLists/Details/5

        public ViewResult Details(int id)
        {
            return View(playlistRepository.Find(id));
        }

        //
        // GET: /PlayLists/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /PlayLists/Create

        [HttpPost]
        public ActionResult Create(PlayList playlist)
        {
            if (ModelState.IsValid) {
                playlistRepository.InsertOrUpdate(playlist);
                playlistRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /PlayLists/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(playlistRepository.Find(id));
        }

        //
        // POST: /PlayLists/Edit/5

        [HttpPost]
        public ActionResult Edit(PlayList playlist)
        {
            if (ModelState.IsValid) {
                playlistRepository.InsertOrUpdate(playlist);
                playlistRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /PlayLists/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(playlistRepository.Find(id));
        }

        //
        // POST: /PlayLists/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            playlistRepository.Delete(id);
            playlistRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                playlistRepository.Dispose();
            }
            base.Dispose(disposing);
        }
         [WebApiOutputCache(120, 60, false)]
        public ActionResult AddToPlayList(int trackId, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                MusicRoomContext context = new MusicRoomContext();
                PlayListRepository playListRepository = new PlayListRepository();
                PlayList playList = new PlayList();
                foreach (var data in context.Tracks)
                {
                    if (data.TrackId == trackId)
                    {
                        playList.TrackPath = data.FileName;
                        playList.Author = data.Author;
                        playList.TrackName = data.TrackName;
                    }
                }
                playList.UserId = WebSecurity.CurrentUserId;
                
                playListRepository.InsertOrUpdate(playList);
                playListRepository.Save();

            }
            return Redirect(returnUrl);
        }

         [WebApiOutputCache(120, 60, false)]
        public ActionResult DeleteTrack(int trackId, string returnUrl)
        {
            PlayListRepository playListRepository = new PlayListRepository();
            playListRepository.Delete(trackId);
            playListRepository.Save();
            return Redirect(returnUrl);
        }
    }
}

