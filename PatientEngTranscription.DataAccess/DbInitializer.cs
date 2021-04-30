using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using PatientEngTranscription.Domain;

namespace PatientEngTranscription.DataAccess
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider services)
        {
            PerformMigrations(services);
            var context = services.GetRequiredService<PatientEngTranscriptionContext>();
           // SeedUnitTypes(context);
           // SeedMeasureTypes(context);
        }
        private static void PerformMigrations(IServiceProvider services)
        {
            services
             .GetRequiredService<PatientEngTranscriptionContext>()
             .Database.Migrate();
        }
        //public static void SeedUnitTypes(PatientEngTranscriptionContext context)
        //{
        //    SeedUnitType(context, "cm");
        //    SeedUnitType(context, "kg");
        //}
        //private static void SeedMeasureTypes(PatientEngTranscriptionContext context)
        //{
        //    SeedMeasureType(context, "Weight");
        //    SeedMeasureType(context, "Height");
        //}
        //private static void SeedUnitType(PatientEngTranscriptionContext context, string name)
        //{
        //    //var unitType = context.UnitType.SingleOrDefault(at => at.Name == name);
        //    //if (unitType != null)
        //    //{
        //    //    return;
        //    //}

        //    //unitType = new UnitType
        //    //{
        //    //    Name = name,
        //    //    IsDeleted = false,
        //    //    CreatedDate = DateTime.UtcNow
        //    //};

        //    //context.UnitType.Add(unitType);
        //    //context.SaveChanges();
        //}
        //public static void SeedMeasureType(PatientEngTranscriptionContext context, string name)
        //{
        //    //var measureType = context.MeasureType.SingleOrDefault(et => et.Name == name);
        //    //if (measureType != null)
        //    //{
        //    //    return;
        //    //}
        //    //measureType = new MeasureType
        //    //{
        //    //    Name = name,
        //    //    IsDeleted = false,
        //    //    CreatedDate = DateTime.UtcNow

        //    //};
        //    //context.MeasureType.Add(measureType);
        //    //context.SaveChanges();
        //}
    }
}
