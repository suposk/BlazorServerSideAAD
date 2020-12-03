//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.EntityFrameworkCore;

//namespace Server.Entities.DbContext
//{

//    public class AppVersionContext : DbContext
//    {
//        private readonly string _connectionString;

//        public AppVersionContext() : base()
//        {
//            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
//        }
//        public AppVersionContext(string connectionString) : base()
//        {
//            _connectionString = connectionString;
//            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
//        }

//        public PdeContext(DbContextOptions<AppVersionContext> options)
//           : base(options)
//        {
//            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
//        }

//        public DbSet<AppVersion> AppVersions { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                //sql Lite
//                if (!string.IsNullOrWhiteSpace(_connectionString))
//                    optionsBuilder.UseSqlite(_connectionString);
//            }
//        }
//    }
//}
