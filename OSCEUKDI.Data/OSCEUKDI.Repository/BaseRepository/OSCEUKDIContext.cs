using OSCEUKDI.Entities.Map;
using OSCEUKDI.Entities.Map.OSCEUKDIMap;
using OSCEUKDI.Entities.Models;
using OSCEUKDI.Entities.Models.OSCEUKDI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Repository.BaseRepository
{
    public class OSCEUKDIContext : DbContext
    {
        public OSCEUKDIContext() : base(getConnectionString())
        {
            Database.SetInitializer<OSCEUKDIContext>(new CreateDatabaseIfNotExists<OSCEUKDIContext>());
            //Configuration.LazyLoadingEnabled = false;
        }

        private static string getConnectionString()
        {
            string conn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            return conn;
        }
        public DbSet<User> Users { get; set; }    
        public DbSet<Role> Roles { get; set; }
        public DbSet<MenuRole> MenuRoles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<RuangUjian> RuangUjians { get; set; }      
        public DbSet<Kompetensi> Kompetensis { get; set; }      
        public DbSet<Proyek> Proyeks { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MenuMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new UserMap());         
            modelBuilder.Configurations.Add(new RuangUjianMap());
            modelBuilder.Configurations.Add(new KompetensiMap());
            modelBuilder.Configurations.Add(new ProyekMap());

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<string>().Configure(c => c.HasColumnType("varchar"));
        }
    }
}