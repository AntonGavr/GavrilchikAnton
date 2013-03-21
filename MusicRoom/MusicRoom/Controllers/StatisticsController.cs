using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicRoom.Models;
using WebMatrix.WebData;

namespace MusicRoom.Controllers
{   
    public class StatisticsController : Controller
    {
		private readonly IStatisticRepository statisticRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public StatisticsController() : this(new StatisticRepository())
        {
        }

        public StatisticsController(IStatisticRepository statisticRepository)
        {
			this.statisticRepository = statisticRepository;
        }


        private List<Statistic> statistics = new List<Statistic>();
        StatisticRepository repository = new StatisticRepository();
        [WebApiOutputCache(120, 60, false)]
        [AllowAnonymous]
        public ActionResult Graphics()
        {
            Database.SetInitializer<MusicRoomContext>(null);
            statistics = repository.CurrentStatisticsList(WebSecurity.CurrentUserId).ToList();
            return View(statistics);
        }


        //
        // GET: /Statistics/

        public ViewResult Index()
        {
            return View(statisticRepository.All);
        }

        //
        // GET: /Statistics/Details/5

        public ViewResult Details(int id)
        {
            return View(statisticRepository.Find(id));
        }

        //
        // GET: /Statistics/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Statistics/Create

        [HttpPost]
        public ActionResult Create(Statistic statistic)
        {
            if (ModelState.IsValid) {
                statisticRepository.InsertOrUpdate(statistic);
                statisticRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Statistics/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(statisticRepository.Find(id));
        }

        //
        // POST: /Statistics/Edit/5

        [HttpPost]
        public ActionResult Edit(Statistic statistic)
        {
            if (ModelState.IsValid) {
                statisticRepository.InsertOrUpdate(statistic);
                statisticRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Statistics/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(statisticRepository.Find(id));
        }

        //
        // POST: /Statistics/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            statisticRepository.Delete(id);
            statisticRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                statisticRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

