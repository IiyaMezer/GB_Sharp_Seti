using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBases.Models
{
    public class StartUp
    {
        public void ConfigureServices(IServiceCollection services)
                => services.AddDbContextPool<ChatContext>(
                    options => options.UseSqlServer(@"Server=.; Database=GB;Integrated Security=False;TrustServerCertificate=True; Trusted_Connection=True;").UseLazyLoadingProxies());

    }
}
