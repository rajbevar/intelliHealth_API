using Microsoft.EntityFrameworkCore;
using PatientEngTranscription.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.DomainLogic
{
    public interface IPatientRepository
    {
        Task<Patient> Get(string id);
        Task<IPagedList<Patient>> GetAll();
        Task<Patient> GetByEmail(string email);
        Task<ResultDto<Guid, PatientCreateStatus>> Create(Patient request);
        Task<ResultDto<Guid, PatientUpdateStatus>> Update(Patient request);
        Task<PatientUpdateStatus> Delete(IList<string> ids);
    }
}
