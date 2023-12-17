using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    internal class ChatContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=GB;Trusted_Connection=True;");
        }
        public ChatContext(DbSet<User> users, DbSet<Message> messages)
        {
            Users = users;
            Messages = messages;
        }
    }
}
