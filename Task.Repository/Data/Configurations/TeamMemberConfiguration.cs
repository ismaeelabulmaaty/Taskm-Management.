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
    public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            builder.Property(M => M.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(M => M.Email)
                   .IsRequired();
        }
    }
}
