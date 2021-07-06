using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace VirtualDmScreen.Models
{
  public class VirtualDmScreenContextFactory : IDesignTimeDbContextFactory<VirtualDmScreenContext>
  {
    VirtualDmScreenContext IDesignTimeDbContextFactory<VirtualDmScreenContext>.CreateDbContext(string[] args)
    {
      IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();
      
      var builder = new DbContextOptionsBuilder<VirtualDmScreenContext>();

      builder.UseMySql(configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"]));

      return new VirtualDmScreenContext(builder.Options);
    }
  }
}