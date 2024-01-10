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
    public class ProyekMap : EntityTypeConfiguration<Proyek>
    {
        public ProyekMap()
        {
            ToTable("Proyek");
            HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
