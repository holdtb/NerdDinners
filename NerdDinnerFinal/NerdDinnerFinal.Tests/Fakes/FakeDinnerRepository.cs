﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdDinnerFinal.Models;

namespace NerdDinnerFinal.Tests.Fakes
{
    class FakeDinnerRepository : IDinnersRepository
    {

        private List<Dinner> dinnerList; 

        public FakeDinnerRepository(List<Dinner> dinners)
        {
            dinnerList = dinners;
        }

        public IQueryable<Dinner> FindAllDinners()
        {
            return dinnerList.AsQueryable();
        }

        public IQueryable<Dinner> FindUpcomingDinners()
        {
            return (from dinner in dinnerList
                    where dinner.EventDate > DateTime.Now
                    select dinner).AsQueryable();    
        }

        public IQueryable<Dinner> FindByLocation(float lat, float lon)
        {
            return (from dinner in dinnerList
                    where dinner.Latitude == lat && dinner.Longitude == lon
                    select dinner).AsQueryable();
        }

        public Dinner GetDinner(int id)
        {
            return dinnerList.SingleOrDefault(d => d.DinnerId == id);
        }

        public void Add(Dinner dinner)
        {
            dinnerList.Add(dinner);
        }

        public void Delete(Dinner dinner)
        {
            dinnerList.Remove(dinner);
        }

        public void Save()
        {
            foreach (Dinner dinner in dinnerList)
            {
                if (!dinner.IsValid())
                    throw new ApplicationException("Rule violations");
            }
        }
    }
}
