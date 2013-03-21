using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MusicRoom.Models
{ 
    public class LikeRepository : ILikeRepository
    {
        MusicRoomContext context = new MusicRoomContext();

        public IQueryable<Like> All
        {
            get { return context.Likes; }
        }

        public IQueryable<Like> AllIncluding(params Expression<Func<Like, object>>[] includeProperties)
        {
            IQueryable<Like> query = context.Likes;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Like Find(int id)
        {
            return context.Likes.Find(id);
        }

        public void InsertOrUpdate(Like like)
        {
            if (like.LikeId == default(int)) {
                // New entity
                context.Likes.Add(like);
            } else {
                // Existing entity
                context.Entry(like).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var like = context.Likes.Find(id);
            context.Likes.Remove(like);
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

    public interface ILikeRepository : IDisposable
    {
        IQueryable<Like> All { get; }
        IQueryable<Like> AllIncluding(params Expression<Func<Like, object>>[] includeProperties);
        Like Find(int id);
        void InsertOrUpdate(Like like);
        void Delete(int id);
        void Save();
    }
}