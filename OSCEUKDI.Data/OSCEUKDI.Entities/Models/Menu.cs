using OSCEUKDI.Entities.Basentities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSCEUKDI.Entities.Models
{
    public class Menu : BaseEntity
    {
        public string MenuName { get; set; }
        public string MenuDescription { get; set; }

        public string MenuUrl { get; set; }
        public string MenuParent { get; set; }
        public string MenuIcon { get; set; }
        public string MenuOrder { get; set; }
        [JsonIgnore]
        public virtual ICollection<MenuRole> MenuRoles { get; set; }
    }
}
