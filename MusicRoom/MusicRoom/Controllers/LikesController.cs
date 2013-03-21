using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicRoom.Models;

namespace MusicRoom.Controllers
{   
    public class LikesController : Controller
    {
		private readonly ITrackRepository trackRepository;
		private readonly ILikeRepository likeRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public LikesController() : this(new TrackRepository(), new LikeRepository())
        {
        }

        public LikesController(ITrackRepository trackRepository, ILikeRepository likeRepository)
        {
			this.trackRepository = trackRepository;
			this.likeRepository = likeRepository;
        }

        //
        // GET: /Likes/

        public ViewResult Index()
        {
            return View(likeRepository.All);
        }

        //
        // GET: /Likes/Details/5

        public ViewResult Details(int id)
        {
            return View(likeRepository.Find(id));
        }

        //
        // GET: /Likes/Create

        public ActionResult Create()
        {
			ViewBag.PossibleTracks = trackRepository.All;
            return View();
        } 

        //
        // POST: /Likes/Create

        [HttpPost]
        public ActionResult Create(Like like)
        {
            if (ModelState.IsValid) {
                likeRepository.InsertOrUpdate(like);
                likeRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleTracks = trackRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Likes/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleTracks = trackRepository.All;
             return View(likeRepository.Find(id));
        }

        //
        // POST: /Likes/Edit/5

        [HttpPost]
        public ActionResult Edit(Like like)
        {
            if (ModelState.IsValid) {
                likeRepository.InsertOrUpdate(like);
                likeRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleTracks = trackRepository.All;
				return View();
			}
        }

        //
        // GET: /Likes/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(likeRepository.Find(id));
        }

        //
        // POST: /Likes/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            likeRepository.Delete(id);
            likeRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                trackRepository.Dispose();
                likeRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

