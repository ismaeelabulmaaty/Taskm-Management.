using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskm.core.Entites;
using taskm.Core.Entites;

namespace taskm.Repository.Dtos
{
    public class TaskDto 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public int TeamMemberId { get; set; }  

    }
}
