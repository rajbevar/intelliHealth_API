using Microsoft.EntityFrameworkCore;
using PatientEngTranscription.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.DomainLogic
{
    public class NotesRepository: INotesRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Notes> _repository;

        public NotesRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Notes>();
        }

        public async Task<ResultDto<Guid, NotesCreateStatus>> Create(Notes request)
        {
            request.CreateDateTime = DateTime.Now;
            _repository.Insert(request);
            await _unitOfWork.SaveChangesAsync();
            return new ResultDto<Guid, NotesCreateStatus>(NotesCreateStatus.Success, request.Id);
        }

        public async Task<ResultDto<Guid, NotesUpdateStatus>> Delete(Guid id)
        {
            var existingObjectResult = await Get(id);
            if (existingObjectResult == null)
            {
                return new ResultDto<Guid, NotesUpdateStatus>(NotesUpdateStatus.NotFound, id);
            }
            
            _repository.Delete(existingObjectResult);

            await _unitOfWork.SaveChangesAsync();
            return new ResultDto<Guid, NotesUpdateStatus>(NotesUpdateStatus.Success, id);
        }

        public async Task<Notes> Get(Guid id)
        {
            var result = await _repository.GetFirstOrDefaultAsync(x => x.Id == id, null, null, false);

            return result;
        }

        
        public async Task<IPagedList<Notes>> GetAllParsedNotes(string externalId)
        {
            var result = await _repository.GetPagedListAsync((x => x.ExternalId == externalId && x.isParsed ), null, null, 0, 20, false);
            //var result = await _repository.GetPagedListAsync();

            return result;
        }

        public async Task<IPagedList<Notes>> GetAll(string externalId)
        {
            var result = await _repository.GetPagedListAsync((x => x.ExternalId == externalId && !x.isParsed && !x.isIgnored), null, null, 0, 20, false);
            //var result = await _repository.GetPagedListAsync();

            return result;
        }

        public async Task<ResultDto<Guid, NotesUpdateStatus>> Update(Notes request)
        {
            var checkAlreadyExist = await Get(request.Id);
            if (checkAlreadyExist != null)
            {
                if (checkAlreadyExist.Id != request.Id)
                {
                    return new ResultDto<Guid, NotesUpdateStatus>(NotesUpdateStatus.AlreadyExist);
                }
            }
            var existingObjectResult = await Get(request.Id);
            if (existingObjectResult == null)
            {
                return new ResultDto<Guid, NotesUpdateStatus>(NotesUpdateStatus.NotFound, request.Id);
            }

            existingObjectResult.Note = request.Note;
            existingObjectResult.DoctorId = request.DoctorId;
            existingObjectResult.CreateDateTime = request.CreateDateTime;


            _repository.Update(existingObjectResult);
            await _unitOfWork.SaveChangesAsync();
            return new ResultDto<Guid, NotesUpdateStatus>(NotesUpdateStatus.Success, request.Id);
        }
    }
}
