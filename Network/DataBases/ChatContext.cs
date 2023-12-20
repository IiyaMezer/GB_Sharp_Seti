using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBases;

public class ChatContext: DbContext

{
    public ChatContext(DbContextOptions<ChatContext> options) : base(options)
    {

    }
    public ChatContext() 
    {

    }
    public  DbSet<User> Users { get; set; }
    public  DbSet<Message> Messages { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users"); 

            entity.HasKey(x => x.Id).HasName("users_pkey");
            entity.HasIndex(x => x.Fullname).IsUnique();

            

            entity.Property(e => e.Fullname).HasColumnName("FullName").
            HasMaxLength(255).IsRequired();
            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(x => x.MessageId).HasName("messages_pkey");
            entity.ToTable("messages");

            entity.Property(e => e.Text).HasColumnName("message_text");
            entity.Property(e => e.Time).HasColumnName("message_date");
            entity.Property(e => e.IsSent).HasColumnName("is_sent");
            entity.Property(e => e.MessageId).HasColumnName("id");

            entity.HasOne(x => x.RecieverId).WithMany(m => m.MessagesTo);
            entity.HasOne(x => x.SenderId).WithMany(m => m.MessagesFrom)
            .HasForeignKey(x=> x.SenderId)
            .HasConstraintName("message_from_user_foreign_key");


        });
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.; Database=GB;Integrated Security=False;TrustServerCertificate=True; Trusted_Connection=True;").UseLazyLoadingProxies();
    }


}

public class ChatContextFactory : IDesignTimeDbContextFactory<ChatContext>
{
    public ChatContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ChatContext>();

        return new ChatContext(optionsBuilder.Options);
    }
}