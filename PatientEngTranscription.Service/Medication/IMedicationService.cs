using PatientEngTranscription.Domain;
using PatientEngTranscription.Domain.MedicalEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.Service
{
    public interface IMedicationService
    {
        Task<MedicationDto> Get(Guid id);
        Task<PagedResult<MedicationDto>> GetAll(string externalId);
        Task<ResultDto<Guid, MedicationCreateStatus>> Create(MedicationEntity request, string externalId);
        Task<ResultDto<Guid, MedicationUpdateStatus>> Update(Guid id, MedicationUpdateRequest request);
        Task<ResultDto<Guid, MedicationUpdateStatus>> Delete(Guid id);

        Task<PagedResult<MedicationDto>> GetWithFollowup(string externalId);
    }
}
