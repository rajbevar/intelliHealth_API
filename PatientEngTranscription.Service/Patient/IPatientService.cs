using PatientEngTranscription.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.Service
{
    public interface IPatientService
    {
        Task<PatientDto> Get(string id);
        Task<PatientDto> GetByEmail(string email);
        Task<PagedResult<PatientDto>> GetAll();
        Task<ResultDto<Guid, PatientCreateStatus>> Create(PatientCreateRequest request);
        Task<ResultDto<Guid, PatientUpdateStatus>> Update(Guid id, PatientUpdateRequest request);
        Task<PatientUpdateStatus> Delete(IList<string> ids);
    }
}
