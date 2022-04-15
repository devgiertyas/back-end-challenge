using TodoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoAPI.Map
{
    public class TodoMap : BaseMap<Todo>
    {

        public TodoMap() : base("todo")
        { }

        public override void Configure(EntityTypeBuilder<Todo> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Category).HasColumnName("category").IsRequired();
            builder.Property(x => x.Title).HasColumnName("title").IsRequired();
            builder.Property(x => x.Project).HasColumnName("project").IsRequired();
            builder.Property(x => x.Description).HasColumnName("description").IsRequired();
            builder.Property(x => x.Category).HasColumnName("category").IsRequired();
            builder.Property(x => x.CreatedDate).HasColumnName("created_date").IsRequired();

            builder.Property(x => x.ExpectedDate).HasColumnName("expected_date").IsRequired();
            builder.Property(x => x.Status).HasColumnName("status").IsRequired();

            builder.Property(x => x.Sequence).HasColumnName("sequence").IsRequired();
            builder.Property(x => x.Time).HasColumnName("time").IsRequired();


        }

    }

}
