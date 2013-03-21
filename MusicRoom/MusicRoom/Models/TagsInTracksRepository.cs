using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MusicRoom.Models
{ 
    public class TagsInTracksRepository : ITagsInTracksRepository
    {
        MusicRoomContext context = new MusicRoomContext();

        public IQueryable<TagsInTracks> All
        {
            get { return context.TagsInTracks; }
        }

        public IQueryable<TagsInTracks> AllIncluding(params Expression<Func<TagsInTracks, object>>[] includeProperties)
        {
            IQueryable<TagsInTracks> query = context.TagsInTracks;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public TagsInTracks Find(int id)
        {
            return context.TagsInTracks.Find(id);
        }

        public void InsertOrUpdate(TagsInTracks tagsintracks)
        {
            if (tagsintracks.Id == default(int)) {
                // New entity
                context.TagsInTracks.Add(tagsintracks);
            } else {
                // Existing entity
                context.Entry(tagsintracks).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var tagsintracks = context.TagsInTracks.Find(id);
            context.TagsInTracks.Remove(tagsintracks);
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

    public interface ITagsInTracksRepository : IDisposable
    {
        IQueryable<TagsInTracks> All { get; }
        IQueryable<TagsInTracks> AllIncluding(params Expression<Func<TagsInTracks, object>>[] includeProperties);
        TagsInTracks Find(int id);
        void InsertOrUpdate(TagsInTracks tagsintracks);
        void Delete(int id);
        void Save();
    }
}