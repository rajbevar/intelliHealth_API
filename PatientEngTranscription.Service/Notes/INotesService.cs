using PatientEngTranscription.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.Service
{
    public interface INotesService
    {
        Task<NotesDto> Get(Guid id);
        Task<PagedResult<NotesDto>> GetAll(string externalId);
        Task<PagedResult<NotesDto>> GetAllParsedNotes(string externalId);
        Task<ResultDto<Guid, NotesCreateStatus>> Create(NotesCreateRequest request);
        Task<ResultDto<Guid, NotesUpdateStatus>> Update(Guid id, NotesUpdateRequest request);
        Task<ResultDto<Guid, NotesUpdateStatus>> Delete(Guid id);
        Task<ResultDto<Guid, NotesUpdateStatus>> UpdateParseStatus(Guid id, bool ignore);
    }
}
