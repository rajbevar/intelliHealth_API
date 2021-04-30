using Microsoft.EntityFrameworkCore;
using PatientEngTranscription.Domain;
using PatientEngTranscription.Domain.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.DomainLogic
{
    public interface IProblemRepository
    {       
        Task<IPagedList<Problems>> GetAll(string externalId, int category);
        Task<ResultDto<Guid, PatientCreateStatus>> Create(Problems request);
       
    }
}
