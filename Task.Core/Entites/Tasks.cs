using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskm.core.Entites;

namespace taskm.Core.Entites
{
    public class Tasks :BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        //[ForeignKey("TeamMember")]
        public int TeamMemberId { get; set; }  //Fk
        public TeamMember TeamMember { get; set; }
    }
}
