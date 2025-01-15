using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDo.Infra.Data.Configurations
{
    public class ToDoConfiguration : IEntityTypeConfiguration<Domain.Entities.ToDo>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ToDo> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(e => e.Desc)
                .IsRequired();

            builder.Property(e => e.Completed);

        }

    }
}
