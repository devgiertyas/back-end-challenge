using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoAPI.Models;

namespace TodoAPI.Map
{
    public class UserMap : BaseMap<User>
    {
        public UserMap() : base("user")
        { }

        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Email).HasColumnName("email").IsRequired();
            builder.Property(x => x.CreatedDate).HasColumnName("created_date").IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(x => x.Name).HasColumnName("name").IsRequired();
            builder.Property(x => x.Password).HasColumnName("password").IsRequired();

            //Insert User Admin

            builder.HasData(new User { Id = 1, Email = "admin@admin.com", Name = "admin", Password = "82b83e666a49d8a95c424330bb4edfc8" });

        }
    }
}
