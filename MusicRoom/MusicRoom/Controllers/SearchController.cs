using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicRoom.Models;
using MusicRoom.Repository;

namespace MusicRoom.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
         [WebApiOutputCache(120, 60, false)]
        public ActionResult Index
       (string searchTerm, string searchField, int? limit)
        {
            // create default Lucene search index directory
            if (!Directory.Exists(GoLucene._luceneDir)) Directory.CreateDirectory(GoLucene._luceneDir);

            // perform Lucene search
            List<TrackData> _searchResults;
            
                _searchResults = (string.IsNullOrEmpty(searchField)
                                   ? GoLucene.Search(searchTerm)
                                   : GoLucene.Search(searchTerm, searchField)).ToList();
            if (string.IsNullOrEmpty(searchTerm) && !_searchResults.Any())
                _searchResults = GoLucene.GetAllIndexRecords().ToList();


            // setup and return view model
            var search_field_list = new
                List<SelectedList> {
				                     	new SelectedList {Text = "(All Fields)", Value = ""},
				                     	new SelectedList {Text = "Id", Value = "Id"},
				                     	new SelectedList {Text = "Author", Value = "Author"},
				                     	new SelectedList {Text = "TrackName", Value = "TrackName"},
                                        new SelectedList {Text = "Tags", Value = "Tags"}
				                     };

            // limit display number of database records
            var limitDb = limit == null ? 3 : Convert.ToInt32(limit);
            List<TrackData> allTrackData;
            if (limitDb > 0)
            {
                allTrackData = TrackDataRepository.GetAll().ToList().Take(limitDb).ToList();
                ViewBag.Limit = TrackDataRepository.GetAll().Count - limitDb;
            }
            else allTrackData = TrackDataRepository.GetAll();

            return View(new IndexLuceneTrackModel
            {
                AllTrackData = allTrackData,
                SearchIndexData = _searchResults,
                SearchFieldList = search_field_list,
            });
        }

        public FileResult Download(string id)
        {
            DownloadModel objData = new DownloadModel();
            int fid = Convert.ToInt32(id);
            var files = objData.GetFiles();
            var file = files.Single(t => fid == t.TrackId);
            var trackName = file.Author + " - " + file.TrackName + ".mp3";
            var fileName = file.FileName;
            string contentType = "application/mp3";
            return File(fileName, contentType, trackName);
        }

        public ActionResult Search(string searchTerm, string searchField)
        {
            return RedirectToAction("Index", new { searchTerm, searchField});
        }

        [ChildActionOnly]
        [WebApiOutputCache(120, 60, false)]
        public ActionResult SearchForm()
        {
            return PartialView();
        }
    }
}
