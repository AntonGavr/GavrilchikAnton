using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicRoom.Models
{
    public class Statistic
    {
        [Key]
        public virtual int StatisticId { get; set; }
        public virtual int UserId { get; set; }
        public virtual DateTime Time { get; set; }
        public virtual int CountLikes { get; set; }
    }
}