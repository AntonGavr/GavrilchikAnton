using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarkdownSharp;
using MusicRoom.Models;
using WebMatrix.WebData;

namespace MusicRoom.Controllers
{
    public class UploadTrackController : Controller
    {
        //
        // GET: /UploadTrack/
        private static string fileName;
         [WebApiOutputCache(120, 60, false)]
        public ActionResult Index()
        {
            return View();
        }
         [WebApiOutputCache(120, 60, false)]
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            string filename = null;
            string path = AppDomain.CurrentDomain.BaseDirectory + @"UploadedFiles\";
            if (file != null) filename = Path.GetFileName(file.FileName);
            string name = Path.Combine(path, filename);
            if (filename != null) file.SaveAs(name);
            fileName = filename;
            return RedirectToAction("Upload");
        }


        private UploadTrack trackInfo = new UploadTrack();


        public ActionResult UploadWithDragAndDrop(HttpPostedFileBase files)
        {
            string filename = null;
            string path = AppDomain.CurrentDomain.BaseDirectory + @"UploadedFiles\";
            if (files != null) filename = Path.GetFileName(files.FileName);
            string name = Path.Combine(path, filename);
            if (filename != null) files.SaveAs(name);
            fileName = filename;
            System.IO.File.WriteAllBytes(name, ReadData(files.InputStream));
            return Json("Well...");
        }

        private byte[] ReadData(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

        [HttpGet]
        public ActionResult Upload()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"UploadedFiles\";
            string name = Path.Combine(path, fileName);
            TagLib.ByteVector.UseBrokenLatin1Behavior = true;
            TagLib.File fileMp3 = TagLib.File.Create(name);
            trackInfo.Author = fileMp3.Tag.Title;
            trackInfo.TrackName = fileMp3.Tag.JoinedArtists;
            trackInfo.Tags = fileMp3.Tag.JoinedGenres;
            trackInfo.Description = fileMp3.Tag.JoinedArtists; 
            return View(trackInfo);
        }

        [HttpPost]
        public ActionResult Upload(UploadTrack trackInfo)
        {
            Track track = new Track();
            track.TrackName = trackInfo.TrackName;
            track.Author = trackInfo.Author;
            track.UserName = WebSecurity.CurrentUserName;
            track.FileName = fileName;
            track.Like = 0;
            track.Listenings = 0;
            track.Description = new Markdown().Transform(trackInfo.Description);
            TrackRepository trackRepository = new TrackRepository();
            trackRepository.InsertOrUpdate(track);
            var tagList = trackInfo.Tags.Split(',');
            TagRepository tagRepository = new TagRepository();
            TagsInTracks tagsInTracks;
            Tag tag;
            TagsInTracksRepository tagInTrackRepository = new TagsInTracksRepository();
            foreach(var tagName in tagList)
            {
                tagsInTracks = new TagsInTracks();
                tagsInTracks.Track = track;
                if (tagRepository.TagExist(tagName))
                {
                    tagsInTracks.TagId = tagRepository.TagSearchId(tagName);
                }
                else
                {
                    tag = new Tag();
                    tag.TagName = tagName;
                    tagRepository.InsertOrUpdate(tag);
                    tagsInTracks.Tag = tag;   
                }
                tagInTrackRepository.InsertOrUpdate(tagsInTracks);
                tagInTrackRepository.Save();
                
            }
            TrackData trackIndex = new TrackData();
            trackIndex.Author = trackInfo.Author;
            trackIndex.Id = track.TrackId;
            trackIndex.Tags = trackInfo.Tags;
            trackIndex.TrackName = trackInfo.TrackName;
            trackIndex.TrackPath = fileName;
            AddToIndex(trackIndex);
     

            return RedirectToAction("Index", "Home");
        }

        public void AddToIndex(TrackData trackData)
        {
            GoLucene.AddUpdateLuceneIndex(trackData);
        }

        [ChildActionOnly]
        [WebApiOutputCache(120, 60, false)]
        public ActionResult DrugAndDrop()
        {
            return PartialView();
        }

    }
}
