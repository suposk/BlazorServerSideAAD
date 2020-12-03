using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Server.Entities.DbContext
{

    public class AppVersionContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private readonly string _connectionString;

        public AppVersionContext() : base()
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public AppVersionContext(string connectionString) : base()
        {
            _connectionString = connectionString;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public AppVersionContext(DbContextOptions<AppVersionContext> options)
           : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<AppVersion> AppVersions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //sql Lite
                if (!string.IsNullOrWhiteSpace(_connectionString))
                {
                    //optionsBuilder.UseSqlite(_connectionString);
                }
            }

            //optionsBuilder.Entity<AppVersion>().HasData(
            //    new AppVersion()
            //    {
            //        Id = 22, 
            //        CreatedAt = 
            //    },
            //    new AppVersion()
            //    {
            //        AddressId = 2,
            //        Address1 = "Ulica 22",
            //        City = "Trstena",
            //        Country = "SK",
            //        PostalCode = "11222",
            //        StateProvince = "ZA"
            //    });
        }
    }
}
