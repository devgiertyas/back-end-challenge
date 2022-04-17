using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoAPI.Models;

namespace TodoAPI.Map
{
    public class TodoUserMap : BaseMap<TodoUser>
    { 
        public TodoUserMap() : base("todo_user")
        {

        }

        public override void Configure(EntityTypeBuilder<TodoUser> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.UserId).HasColumnName("id_user").IsRequired();
            builder.Property(x => x.TodoId).HasColumnName("id_todo").IsRequired();

            builder.HasKey(x => new { x.UserId, x.TodoId });

            builder.HasOne<User>(sc => sc.User)
            .WithMany(s => s.TodoUsers)
            .HasForeignKey(sc => sc.UserId).OnDelete(DeleteBehavior.Cascade); ;

            builder.HasOne<Todo>(sc => sc.Todo)
           .WithMany(s => s.TodoUsers)
           .HasForeignKey(sc => sc.TodoId).OnDelete(DeleteBehavior.Cascade); ;

        }

    }
}
