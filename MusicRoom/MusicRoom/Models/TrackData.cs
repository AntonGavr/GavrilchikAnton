using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicRoom.Models
{
    public class TrackData
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string TrackName { get; set; }
        public string Tags { get; set; }
        public string TrackPath { get; set; }
        public int Like { get; set; }
    }
}