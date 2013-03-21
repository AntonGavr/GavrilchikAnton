using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MusicRoom.Models
{
    public class MusicRoomContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<MusicRoom.Models.MusicRoomContext>());

        public DbSet<MusicRoom.Models.Track> Tracks { get; set; }

        public DbSet<MusicRoom.Models.Tag> Tags { get; set; }

        public DbSet<MusicRoom.Models.TagsInTracks> TagsInTracks { get; set; }

        public DbSet<MusicRoom.Models.Like> Likes { get; set; }

        public DbSet<MusicRoom.Models.PlayList> PlayLists { get; set; }

        public DbSet<MusicRoom.Models.Statistic> Statistics { get; set; }
    }
}