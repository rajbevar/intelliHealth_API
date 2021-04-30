using AutoMapper;
using PatientEngTranscription.Domain;
using PatientEngTranscription.Domain.DbEntities;
using PatientEngTranscription.Domain.MedicalEntity;
using PatientEngTranscription.Domain.Models;
using PatientEngTranscription.Domain.Models.Problems;

namespace PatientEngTranscription.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Users, UsersDto>().ReverseMap();
            CreateMap<UsersCreateRequest, Users>();
            CreateMap<UsersUpdateRequest, Users>();


            CreateMap<Patient, PatientDto>().ReverseMap();
            CreateMap<PatientCreateRequest, Patient>();
            CreateMap<PatientUpdateRequest, Patient>();

            CreateMap<Medication, MedicationDto>().ReverseMap();

            CreateMap<MedicationCreateRequest, Medication>()
                .ForMember(s => s.Duration, o => o.MapFrom(d => d.DurationInNumber))
                .ForMember(s => s.Name, o => o.MapFrom(d => d.Name))
                .ForMember(s => s.Doage, o => o.MapFrom(d => d.Doage))
                .ForMember(s => s.Frenquency, o => o.MapFrom(d => d.Frenquency))
                .ForMember(s => s.Strength, o => o.MapFrom(d => d.Strength))
                .ForMember(s => s.Code, o => o.MapFrom(d => d.Name))
                .ForMember(s => s.FrenquencyInDay, o => o.MapFrom(d => d.FrenquencyInDay))                
                 .ForAllOtherMembers(d => d.Ignore());

            CreateMap<MedicationUpdateRequest, Medication>();

            CreateMap<Notes, NotesDto>().ReverseMap();
            CreateMap<NotesCreateRequest, Notes>();
            CreateMap<NotesUpdateRequest, Notes>();

            CreateMap<Problems, ProblemDto>().ReverseMap();
            CreateMap<ProblemCreateRequest, Problems>();

            CreateMap<MedicationFollowUp, MedicationFollowupDto>()
                 .ForMember(s => s.MedicationId, o => o.MapFrom(d => d.MedicationId))
                 .ForMember(s => s.Status, o => o.MapFrom(d => d.Status))
                 .ForMember(s => s.TakenDate, o => o.MapFrom(d => d.TakenDate))
                 .ForMember(s => s.TakenTime, o => o.MapFrom(d => d.TakenTime))
             .ForAllOtherMembers(d => d.Ignore());

            CreateMap<MedicationEntity, MedicationCreateRequest>()
                .ForMember(s => s.Name, o => o.MapFrom(d => d.Text))
                .ForMember(s => s.Doage, o => o.MapFrom(d => d.Dosage))
                .ForMember(s => s.Frenquency, o => o.MapFrom(d => d.Frequency))
                .ForMember(s => s.Strength, o => o.MapFrom(d => d.Strength))
                .ForMember(s => s.Code, o => o.MapFrom(d => d.Text))
                .ForMember(s => s.DurationInNumber, o => o.MapFrom(d => d.DurationInNumber))
                .ForMember(s => s.FrenquencyInDay, o => o.MapFrom(d => d.FrenquencyInDay))
                 .ForAllOtherMembers(d => d.Ignore());


        }
    }
}
