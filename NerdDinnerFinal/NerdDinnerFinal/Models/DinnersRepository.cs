using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity.Core.Objects;

namespace NerdDinnerFinal.Models
{
    public class DinnersRepository : IDinnersRepository
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

        public IQueryable<Dinner> FindByLocation(float latitude, float longitude)
        {

            var dinners = from dinner in FindUpcomingDinners()
                          join i in db.NearestDinners(latitude, longitude)
                          on dinner.DinnerId equals i.DinnerId
                          select dinner;

            return dinners;
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