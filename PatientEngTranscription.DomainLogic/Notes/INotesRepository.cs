using Microsoft.EntityFrameworkCore;
using PatientEngTranscription.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.DomainLogic
{
    public interface INotesRepository
    {
        Task<Notes> Get(Guid id);
        Task<IPagedList<Notes>> GetAll(string externalId);
        Task<IPagedList<Notes>> GetAllParsedNotes(string externalId);
        Task<ResultDto<Guid, NotesCreateStatus>> Create(Notes request);
        Task<ResultDto<Guid, NotesUpdateStatus>> Update(Notes request);
        Task<ResultDto<Guid, NotesUpdateStatus>> Delete(Guid id);
    }
}
