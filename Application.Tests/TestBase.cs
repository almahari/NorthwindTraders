﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NorthwindTraders.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Tests
{
    public class TestBase
    {
        private bool useSqlite;

        public NorthwindContext GetDbContext()
        {
            var builder = new DbContextOptionsBuilder<NorthwindContext>();
            if (useSqlite)
            {
                // Use Sqlite DB.
                builder.UseSqlite("DataSource=:memory:", x => { });
            }
            else
            {
                // Use In-Memory DB.
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).ConfigureWarnings(w =>
                {
                    w.Ignore(InMemoryEventId.TransactionIgnoredWarning);
                });
            }

            var dbContext = new NorthwindContext(builder.Options);
            if (useSqlite)
            {
                // SQLite needs to open connection to the DB.
                // Not required for in-memory-database and MS SQL.
                dbContext.Database.OpenConnection();
            }

            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        public void UseSqlite()
        {
            useSqlite = true;
        }
    }
}