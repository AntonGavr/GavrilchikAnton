using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicRoom.Models
{
    public class PlayList
    {
        public virtual int PlayListId { get; set; }
        public virtual int UserId { get; set; }
        public virtual string TrackPath { get; set; }
        public virtual string TrackName { get; set; }
        public virtual string Author { get; set; }
    }
}