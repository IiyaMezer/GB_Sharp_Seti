using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB;

internal class ChatContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
        optionsBuilder.UseSqlServer("Server=localhost;Database=GB;Trusted_Connection=True;").UseLazyLoadingProxies();
    }
    public ChatContext(DbSet<User> users, DbSet<Message> messages)
    {
        Users = users;
        Messages = messages;
    }
    public ChatContext() { }

}
