using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MusicRoom.Models
{
    public class TagsInTracks
    {
        [Key]
        public virtual int Id { get; set; }
        public int? TrackId { get; set; }
        public int? TagId { get; set; }
        [ForeignKey("TrackId")]
        public virtual Track Track { get; set; }
        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
    }
}