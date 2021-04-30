using PatientEngTranscription.Domain;
using PatientEngTranscription.Domain.Models.Problems;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.Service
{
    public interface IProblemService
    {
        
        Task<PagedResult<ProblemDto>> GetAll(string externalId, int category);
        Task<ResultDto<Guid, PatientCreateStatus>> Create(ProblemCreateRequest request,string externalId);
       
    }
}
