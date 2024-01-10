using OSCEUKDI.Entities.Basentities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCEUKDI.Entities.Models.OSCEUKDI
{
    public class Proyek : BaseEntity
    {
        public string Nama { get; set; }
        public string Deskripsi { get; set; }

    }
}
