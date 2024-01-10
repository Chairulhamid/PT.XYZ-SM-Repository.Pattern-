using OSCEUKDI.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.Map
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("User");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.UserName).HasMaxLength(50).IsRequired();
            Property(t => t.Password).HasMaxLength(500).IsRequired();
            Property(t => t.Email).HasMaxLength(150).IsRequired();
          //  Property(t => t.NoPegawai).HasMaxLength(50).IsRequired();

        }
    }
}
