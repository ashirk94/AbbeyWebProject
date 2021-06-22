using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using AbbeyClasses.Models;
using Microsoft.EntityFrameworkCore;
namespace AbbeyTests
{
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
            monks = dbContext.Monk.OrderBy(m => m.LastName).ToList();
            Assert.AreEqual(5, monks.Count);
            Assert.AreEqual("Shrewsbury", monks[0].LastName);
        }
    }
}