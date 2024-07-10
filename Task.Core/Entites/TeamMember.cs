using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskm.core.Entites;


namespace taskm.Core.Entites
{
    public class TeamMember:BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public  ICollection<Tasks> Task { get; set; } = new HashSet<Tasks>();

    }
}
