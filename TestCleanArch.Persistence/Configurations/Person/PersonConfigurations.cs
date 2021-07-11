using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestCleanArch.Persistence.Configurations.Person
{
    public class PersonConfigurations : IEntityTypeConfiguration<Domain.Models.Person>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Person> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Password).IsRequired();
        }
    }
}
