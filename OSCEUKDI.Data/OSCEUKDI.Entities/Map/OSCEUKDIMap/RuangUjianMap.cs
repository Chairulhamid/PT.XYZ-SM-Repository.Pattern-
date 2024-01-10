using OSCEUKDI.Entities.Models.OSCEUKDI;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.Map.OSCEUKDIMap
{
    public class RuangUjianMap : EntityTypeConfiguration<RuangUjian>
    {
        public RuangUjianMap()
        {
            ToTable("RuangUjian");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.NamaRuangan).HasMaxLength(50).IsRequired();
        }
    }
}
