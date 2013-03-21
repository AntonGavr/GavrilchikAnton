using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MusicRoom.Models
{ 
    public class TagRepository : ITagRepository
    {
        MusicRoomContext context = new MusicRoomContext();

        public IQueryable<Tag> All
        {
            get { return context.Tags; }
        }

        public IQueryable<Tag> AllIncluding(params Expression<Func<Tag, object>>[] includeProperties)
        {
            IQueryable<Tag> query = context.Tags;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Tag Find(int id)
        {
            return context.Tags.Find(id);
        }

        public void InsertOrUpdate(Tag tag)
        {
            if (tag.TagId == default(int)) {
                // New entity
                context.Tags.Add(tag);
            } else {
                // Existing entity
                context.Entry(tag).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var tag = context.Tags.Find(id);
            context.Tags.Remove(tag);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }

        public bool TagExist(string tagName)
        {
            //Database.SetInitializer<MusicRoomContext>(null);
            var tagList = context.Tags.ToList<Tag>();
            foreach (var tag in tagList)
            {
                if (tag.TagName == tagName)
                {
                    return true;
                }
            }
            return false;
        }

        public int TagSearchId(string tagName)
        {
            //Database.SetInitializer<MusicRoomContext>(null);
            var tagList = context.Tags.ToList<Tag>();
            foreach (var tag in tagList)
            {
                if (tag.TagName == tagName)
                {
                    return tag.TagId;
                }
            }
            return 0;
        }
    }

    public interface ITagRepository : IDisposable
    {
        IQueryable<Tag> All { get; }
        IQueryable<Tag> AllIncluding(params Expression<Func<Tag, object>>[] includeProperties);
        Tag Find(int id);
        void InsertOrUpdate(Tag tag);
        void Delete(int id);
        void Save();
    }

    
}