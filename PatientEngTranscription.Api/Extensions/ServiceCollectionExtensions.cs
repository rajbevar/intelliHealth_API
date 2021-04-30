using Microsoft.Extensions.DependencyInjection;
using PatientEngTranscription.AWSService;
using PatientEngTranscription.DomainLogic;
using PatientEngTranscription.Service;
using PatientEngTranscription.Service.ClinicalNotesExtraction;
using PatientEngTranscription.Service.FHIRService;

namespace PatientEngTranscription.Api
{
    public static partial class ServiceCollectionExtensions
    {

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Services
         
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IMedicationService, MedicationService>();
            services.AddScoped<INotesService, NotesService>();
            services.AddTransient<ComprehendMedicalService>();
            services.AddTransient<ComprehendMedicalServiceMock>();
            services.AddTransient<ClinicalNotesExtractionService>();
            services.AddTransient<FhirMedicalRequestService>();
            services.AddTransient<FhirConditionService>();
            services.AddTransient<FhirPatientService>();

            services.AddScoped<IProblemService, ProblemService>();
            services.AddTransient<DrugSuggestionAIService>();

            //repository

            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IProblemRepository, ProblemRepository>();
            services.AddTransient<IMedicationRepository, MedicationRepository>();
            services.AddTransient<INotesRepository, NotesRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<MedicationFolloupRepository>();
            services.AddTransient<MedicationFollowupService>();
            
            return services;
        }
    }
}
