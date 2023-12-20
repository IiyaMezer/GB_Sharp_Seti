using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBases
{
    public class ChatContextFactory
    {
        public ChatContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ChatContext>();

            return new ChatContext(optionsBuilder.Options);
        }
    }
}
