using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using AbbeyClasses.Models;
using Microsoft.EntityFrameworkCore;
namespace AbbeyTests
{
    [TestFixture]
    public class Tests
    {
        BuckfastabbeyContext dbContext;
        Monk m;
        List<Monk> monks;

        [SetUp]
        public void Setup()
        {
            dbContext = new BuckfastabbeyContext();
            dbContext.Database.ExecuteSqlRaw("call resetData()");
        }

        [Test]
        public void GetAllTest()
        {
            monks = dbContext.Monk.OrderBy(m => m.MonkId).ToList();
            Assert.AreEqual(5, monks.Count);
            Assert.AreEqual("Shrewsbury", monks[0].LastName);
        }
        [Test]
        public void GetByIDTest()
        {
            m = dbContext.Monk.Find(1);
            Assert.IsNotNull(m);
            Assert.AreEqual("Abbot", m.Job);
        }
        [Test]
        public void DeleteTest()
        {
            m = dbContext.Monk.Find(5);
            dbContext.Monk.Remove(m);
            dbContext.SaveChanges();
            Assert.IsNull(dbContext.Monk.Find(5));
        }

        [Test]
        public void CreateTest()
        {
            m = new Monk
            {
                MonkId = 10,
                FirstName = "Nicholas",
                LastName = "Cage",
                Age = 45,
                Job = "Custodian"
            };
            dbContext.Monk.Add(m);
            dbContext.SaveChanges();
            Assert.IsNotNull(dbContext.Monk.Find(10));
        }

        [Test]
        public void UpdateTest()
        {
            m = dbContext.Monk.Find(3);
            m.FirstName = "Misato";
            m.LastName = "Katsuragi";
            dbContext.Monk.Update(m);
            dbContext.SaveChanges();
            Assert.AreEqual("Misato", m.FirstName);
            Assert.AreEqual("Katsuragi", m.LastName);
        }
    }
}