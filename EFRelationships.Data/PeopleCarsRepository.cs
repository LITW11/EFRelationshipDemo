﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EFRelationships.Data
{
    public class PeopleCarsRepository
    {
        private string _connectionString;

        public PeopleCarsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetPeople()
        {
            using var context = new PeopleCarsContext(_connectionString);
            return context.People.Include(c => c.Cars).ToList();
        }

        public List<Person> GetPeopleWithNoCars()
        {
            //using var context = new PeopleCarsContext(_connectionString);
            //return context.People
            //    .Where(p => p.Cars.Any(c => c.Make == "Lamborghini"))
            //    .Include(c => c.Cars)
            //    .ToList();
            using var context = new PeopleCarsContext(_connectionString);
            return context.People.Include(p => p.Cars).Where(p => p.Cars.Count == 0).ToList();
        }

        public Person GetPerson(int id)
        {
            using var context = new PeopleCarsContext(_connectionString);
            return context.People.FirstOrDefault(p => p.Id == id);
        }

        public void AddCar(Car car)
        {
            using var context = new PeopleCarsContext(_connectionString);
            context.Cars.Add(car);
            context.SaveChanges();
        }
    }
}
