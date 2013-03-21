using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicRoom.Models
{
    public class Tag
    {
        [Key]
        public virtual int TagId { get; set; }
        public virtual string TagName { get; set; }
    }
}