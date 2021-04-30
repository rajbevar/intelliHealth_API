using Microsoft.EntityFrameworkCore;
using PatientEngTranscription.Domain;
using PatientEngTranscription.Domain.DbEntities;

namespace PatientEngTranscription.DataAccess
{
    public class PatientEngTranscriptionContext : DbContext
    {
        public PatientEngTranscriptionContext(DbContextOptions<PatientEngTranscriptionContext> options)
           : base(options)
        {

        }
        public PatientEngTranscriptionContext(string connectionString) : base(GetOptions(connectionString))
        {
        }
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
        
        public DbSet<Users> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medication> Medication { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Problems> Problems { get; set; }
        public DbSet<MedicationFollowUp> MedicationFollowUps { get; set; }

    }
}
