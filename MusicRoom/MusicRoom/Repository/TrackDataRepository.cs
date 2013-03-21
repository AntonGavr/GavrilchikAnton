using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using MusicRoom.Models;

namespace MusicRoom.Repository
{
    public class TrackDataRepository
    {
        public static TrackData Get(int id)
        {
            return GetAll().SingleOrDefault(x => x.Id.Equals(id));
        }

        public static List<TrackData> GetAll()
        {
            MusicRoomContext context = new MusicRoomContext();
            Database.SetInitializer<MusicRoomContext>(null);
            List<TrackData> trackDataList = new List<TrackData>();
            StringBuilder tags = new StringBuilder();
            foreach (var data in context.Tracks)
            {
                tags.Clear();
                foreach (var tag in context.TagsInTracks)
                {
                    if (tag.TrackId == data.TrackId)
                    {
                        tags.Append(tag.Tag.TagName + ",");
                    }
                }
                if (tags.Length > 0)
                {
                    tags.Remove(tags.Length - 1, 1);
                }
                trackDataList.Add(new TrackData { Id = data.TrackId, Author = data.Author, TrackName = data.TrackName, Tags = tags.ToString(), TrackPath = data.FileName, Like = data.Like});
            }
            return trackDataList;
        }
    }
}