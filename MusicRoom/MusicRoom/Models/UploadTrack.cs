using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicRoom.Models
{
    public class UploadTrack
    {
        public string Author { get; set; }
        public string TrackName { get; set; }
        public string Tags { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
    }
}