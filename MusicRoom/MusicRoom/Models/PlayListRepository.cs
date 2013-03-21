using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MusicRoom.Models
{ 
    public class PlayListRepository : IPlayListRepository
    {
        MusicRoomContext context = new MusicRoomContext();

        public IQueryable<PlayList> All
        {
            get { return context.PlayLists; }
        }

        public IQueryable<PlayList> AllIncluding(params Expression<Func<PlayList, object>>[] includeProperties)
        {
            IQueryable<PlayList> query = context.PlayLists;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public PlayList Find(int id)
        {
            return context.PlayLists.Find(id);
        }

        public void InsertOrUpdate(PlayList playlist)
        {
            if (playlist.PlayListId == default(int)) {
                // New entity
                context.PlayLists.Add(playlist);
            } else {
                // Existing entity
                context.Entry(playlist).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var playlist = context.PlayLists.Find(id);
            context.PlayLists.Remove(playlist);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface IPlayListRepository : IDisposable
    {
        IQueryable<PlayList> All { get; }
        IQueryable<PlayList> AllIncluding(params Expression<Func<PlayList, object>>[] includeProperties);
        PlayList Find(int id);
        void InsertOrUpdate(PlayList playlist);
        void Delete(int id);
        void Save();
    }
}