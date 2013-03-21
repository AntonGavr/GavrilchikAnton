using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace MusicRoom.Models
{
    public class DownloadModel
    {
        public List<Track> GetFiles()
        {
            List<Track> lstFiles = new List<Track>();
            DirectoryInfo dirInfo = new DirectoryInfo(HostingEnvironment.MapPath("~/UploadedFiles"));
            int i = 0;
            foreach (var item in dirInfo.GetFiles())
            {
                lstFiles.Add(new Track()
                {
                    TrackId = i + 1,
                    TrackName = item.Name,
                    FileName = dirInfo.FullName + @"\" + item.Name
                });
                i = i + 1;
            }
            return lstFiles;
        }
        
    }
}