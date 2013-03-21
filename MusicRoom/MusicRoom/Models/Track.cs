using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicRoom.Models
{
    public class Track
    {
        [Key]
        public virtual int TrackId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Author { get; set; }
        public virtual string TrackName { get; set; }
        public virtual string Description { get; set; }
        public virtual int Listenings { get; set; }
        public virtual int Like { get; set; }
        public virtual string FileName { get; set; }
    }
}