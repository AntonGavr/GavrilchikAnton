using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebMatrix.WebData;

namespace MusicRoom.Models
{ 
    public class TrackRepository : ITrackRepository
    {
        MusicRoomContext context = new MusicRoomContext();

        public IQueryable<Track> All
        {
            get { return context.Tracks; }
        }

        public IQueryable<Track> AllIncluding(params Expression<Func<Track, object>>[] includeProperties)
        {
            IQueryable<Track> query = context.Tracks;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Track Find(int id)
        {
            return context.Tracks.Find(id);
        }

        public void InsertOrUpdate(Track track)
        {
            if (track.TrackId == default(int)) {
                // New entity
                context.Tracks.Add(track);
            } else {
                // Existing entity
                context.Entry(track).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var track = context.Tracks.Find(id);
            context.Tracks.Remove(track);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }

        public IQueryable<Like> CurrentLikes(int songId)
        {
            return context.Likes.Where(t => t.UserName == WebSecurity.CurrentUserName && t.TrackId == songId);
        }




    }

    public interface ITrackRepository : IDisposable
    {
        IQueryable<Track> All { get; }
        IQueryable<Track> AllIncluding(params Expression<Func<Track, object>>[] includeProperties);
        Track Find(int id);
        void InsertOrUpdate(Track track);
        void Delete(int id);
        void Save();
    }
}