using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicRoom.Models;
using MusicRoom.Repository;
using WebMatrix.WebData;

namespace MusicRoom.Controllers
{   
    public class TracksController : Controller
    {
		private readonly ITrackRepository trackRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public TracksController() : this(new TrackRepository())
        {
        }

        public TracksController(ITrackRepository trackRepository)
        {
			this.trackRepository = trackRepository;
        }

        //
        // GET: /Tracks/

        public ViewResult Index()
        {
            return View(trackRepository.All);
        }

        //
        // GET: /Tracks/Details/5

        public ViewResult Details(int id)
        {
            return View(trackRepository.Find(id));
        }

        //
        // GET: /Tracks/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Tracks/Create

        [HttpPost]
        public ActionResult Create(Track track)
        {
            if (ModelState.IsValid) {
                trackRepository.InsertOrUpdate(track);
                trackRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Tracks/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(trackRepository.Find(id));
        }

        //
        // POST: /Tracks/Edit/5

        [HttpPost]
        public ActionResult Edit(Track track)
        {
            if (ModelState.IsValid) {
                trackRepository.InsertOrUpdate(track);
                trackRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Tracks/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(trackRepository.Find(id));
        }

        //
        // POST: /Tracks/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            trackRepository.Delete(id);
            trackRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                trackRepository.Dispose();
            }
            base.Dispose(disposing);
        }



        private LikeRepository likeRepository = new LikeRepository();
        private TrackRepository trackR = new TrackRepository();
        private StatisticRepository _statisticRepository = new StatisticRepository();
        DateTime date = DateTime.Today;

        public JsonResult Like(int trackId)
        {
            var likes = trackR.CurrentLikes(trackId);
            Track track = trackRepository.Find(trackId);
            var statistic = _statisticRepository.CurrentStatistics(WebSecurity.CurrentUserId, date);
            if (statistic == null)
            {
                CreateNewStatistic(statistic);
            }
            if (likes.Count() == 0)
            {
                track.Like += 1;
                Like like = new Like();
                likeRepository.InsertOrUpdate(like);
                like.TrackId = trackId;
                like.UserName = WebSecurity.CurrentUserName;
                statistic.CountLikes += 1;
                SaveRepository(statistic, like, track);
            }
            else
            {
                var singleOrDefault = likes.SingleOrDefault(t => t.UserName == WebSecurity.CurrentUserName);
                if (singleOrDefault != null)
                {
                    track.Like -= 1;
                    int likeId = singleOrDefault.LikeId;
                    likeRepository.Delete(likeId);
                    SaveRepository(statistic, null, track);
                }
            }
            GoLucene.ClearLuceneIndex();
            GoLucene.AddUpdateLuceneIndex(TrackDataRepository.GetAll());
            return Json(track.Like);
        }

        private void SaveRepository(Statistic statistic, Like like, Track track)
        {
            trackRepository.InsertOrUpdate(track);
            trackRepository.Save();
            likeRepository.Save();
            _statisticRepository.InsertOrUpdate(statistic);
            _statisticRepository.Save();

        }

        private void CreateNewStatistic(Statistic statistic)
        {
            statistic = new Statistic();
            statistic.UserId = WebSecurity.CurrentUserId;
            statistic.CountLikes = 0;
            statistic.Time = date;
        }
    }
}

