using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MusicRoom.Models
{ 
    public class StatisticRepository : IStatisticRepository
    {
        MusicRoomContext context = new MusicRoomContext();

        public IQueryable<Statistic> All
        {
            get { return context.Statistics; }
        }

        public IQueryable<Statistic> AllIncluding(params Expression<Func<Statistic, object>>[] includeProperties)
        {
            IQueryable<Statistic> query = context.Statistics;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Statistic Find(int id)
        {
            return context.Statistics.Find(id);
        }

        public void InsertOrUpdate(Statistic statistic)
        {
            if (statistic.StatisticId == default(int)) {
                // New entity
                context.Statistics.Add(statistic);
            } else {
                // Existing entity
                context.Entry(statistic).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var statistic = context.Statistics.Find(id);
            context.Statistics.Remove(statistic);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }

        public Statistic CurrentStatistics(int userId, DateTime date)
        {
            try
            {
                return context.Statistics.First(t => t.UserId == userId && t.Time == date);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public IQueryable<Statistic> CurrentStatisticsList(int userId)
        {
            return context.Statistics.Where(t => t.UserId == userId);
        }


    }

    public interface IStatisticRepository : IDisposable
    {
        IQueryable<Statistic> All { get; }
        IQueryable<Statistic> AllIncluding(params Expression<Func<Statistic, object>>[] includeProperties);
        Statistic Find(int id);
        void InsertOrUpdate(Statistic statistic);
        void Delete(int id);
        void Save();
    }
}