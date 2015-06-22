using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity.Core.Objects;

namespace NerdDinnerFinal.Models
{
    public class DinnersRepository
    {
        private NerdDinnersDbContext db = new NerdDinnersDbContext();

        //Query Methods
        public IQueryable<Dinner> FindAllDinners()
        {
            return db.Dinners;
        }

        public IQueryable<Dinner> FindUpcomingDinners()
        {
            return from dinner in db.Dinners
                   where dinner.EventDate > DateTime.Now
                   orderby dinner.EventDate
                   select dinner;
        }

        public Dinner GetDinner(int id)
        {
            return db.Dinners.SingleOrDefault(d => d.DinnerId == id);
        }

        //Insert/Delete
        public void Add(Dinner dinner)
        {
            db.Dinners.Add(dinner);
            Save();
        }

        public void Delete(Dinner dinner)
        {
            db.Dinners.Remove(dinner);
            Save();
        }

        //Persistence
        public void Save()
        {
            db.SaveChanges();
        }
    }
}