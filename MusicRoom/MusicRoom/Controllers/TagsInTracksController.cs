using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicRoom.Models;

namespace MusicRoom.Controllers
{   
    public class TagsInTracksController : Controller
    {
		private readonly ITrackRepository trackRepository;
		private readonly ITagRepository tagRepository;
		private readonly ITagsInTracksRepository tagsintracksRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public TagsInTracksController() : this(new TrackRepository(), new TagRepository(), new TagsInTracksRepository())
        {
        }

        public TagsInTracksController(ITrackRepository trackRepository, ITagRepository tagRepository, ITagsInTracksRepository tagsintracksRepository)
        {
			this.trackRepository = trackRepository;
			this.tagRepository = tagRepository;
			this.tagsintracksRepository = tagsintracksRepository;
        }

        //
        // GET: /TagsInTracks/

        public ViewResult Index()
        {
            return View(tagsintracksRepository.AllIncluding(tagsintracks => tagsintracks.Track, tagsintracks => tagsintracks.Tag));
        }

        //
        // GET: /TagsInTracks/Details/5

        public ViewResult Details(int id)
        {
            return View(tagsintracksRepository.Find(id));
        }

        //
        // GET: /TagsInTracks/Create

        public ActionResult Create()
        {
			ViewBag.PossibleTracks = trackRepository.All;
			ViewBag.PossibleTags = tagRepository.All;
            return View();
        } 

        //
        // POST: /TagsInTracks/Create

        [HttpPost]
        public ActionResult Create(TagsInTracks tagsintracks)
        {
            if (ModelState.IsValid) {
                tagsintracksRepository.InsertOrUpdate(tagsintracks);
                tagsintracksRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleTracks = trackRepository.All;
				ViewBag.PossibleTags = tagRepository.All;
				return View();
			}
        }
        
        //
        // GET: /TagsInTracks/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleTracks = trackRepository.All;
			ViewBag.PossibleTags = tagRepository.All;
             return View(tagsintracksRepository.Find(id));
        }

        //
        // POST: /TagsInTracks/Edit/5

        [HttpPost]
        public ActionResult Edit(TagsInTracks tagsintracks)
        {
            if (ModelState.IsValid) {
                tagsintracksRepository.InsertOrUpdate(tagsintracks);
                tagsintracksRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleTracks = trackRepository.All;
				ViewBag.PossibleTags = tagRepository.All;
				return View();
			}
        }

        //
        // GET: /TagsInTracks/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(tagsintracksRepository.Find(id));
        }

        //
        // POST: /TagsInTracks/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            tagsintracksRepository.Delete(id);
            tagsintracksRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                trackRepository.Dispose();
                tagRepository.Dispose();
                tagsintracksRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

