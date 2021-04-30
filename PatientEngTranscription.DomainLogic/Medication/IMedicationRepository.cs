using Microsoft.EntityFrameworkCore;
using PatientEngTranscription.Domain;
using PatientEngTranscription.Domain;
using PatientEngTranscription.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.DomainLogic
{
    public interface IMedicationRepository
    {

        Task<Medication> Get(Guid id);
        Task<IPagedList<Medication>> GetAll(string externalId);
        Task<ResultDto<Guid, MedicationCreateStatus>> Create(Medication request);
        Task<ResultDto<Guid, MedicationUpdateStatus>> Update(Medication request);
        Task<ResultDto<Guid, MedicationUpdateStatus>> Delete(Guid id);
    }
}
