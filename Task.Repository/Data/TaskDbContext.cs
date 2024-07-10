using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using taskm.Core.Entites;

namespace taskm.Repository.Data
{
    public class TaskDbContext :DbContext
    {

        public TaskDbContext(DbContextOptions<TaskDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
    }
}
