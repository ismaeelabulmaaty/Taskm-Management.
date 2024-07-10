using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskm.Core.Entites;

namespace taskm.Repository.Data.Configurations
{
    public class TasksConfigurations : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.HasOne(T => T.TeamMember)
                   .WithMany(M=>M.Task)
                   .HasForeignKey(T => T.TeamMemberId);

            builder.Property(T => T.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(T => T.Description)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(T => T.Status)
                   .IsRequired();
                   

        }
    }
}
