using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PatientEngTranscription.DataAccess
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PatientEngTranscriptionContext>
    {
        public PatientEngTranscriptionContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<PatientEngTranscriptionContext>();
            var connectionString = configuration.GetConnectionString("PatientEngTranscriptionConnection");
            builder.UseSqlServer(connectionString);
            return new PatientEngTranscriptionContext(builder.Options);
        }
    }
}
