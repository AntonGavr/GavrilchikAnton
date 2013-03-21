using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicRoom.Models
{
    public class Like
    {
        [Key]
        public virtual int LikeId { get; set; }
        public virtual string UserName { get; set; }
        public virtual int TrackId { get; set; }
    }
}