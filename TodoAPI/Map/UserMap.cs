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
            builder.Property(x => x.CreatedDate).HasColumnName("created_date").IsRequired();
            builder.Property(x => x.Name).HasColumnName("name").IsRequired();
            
        }
    }
}
