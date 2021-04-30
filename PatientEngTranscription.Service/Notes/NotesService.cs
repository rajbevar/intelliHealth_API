using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatientEngTranscription.Domain;
using PatientEngTranscription.DomainLogic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.Service
{
    public class NotesService: INotesService
    {
        private readonly INotesRepository _repository;

        private readonly IMapper _mapper;

        public NotesService(INotesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResultDto<Guid, NotesCreateStatus>> Create(NotesCreateRequest request)
        {
            var model = _mapper.Map<NotesCreateRequest, Notes>(request);
            model.CreateDateTime = DateTime.UtcNow;
            try
            {
                var result = await _repository.Create(model);

                return result;
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ResultDto<Guid, NotesCreateStatus>(NotesCreateStatus.InternalServerError);
            }
            catch (Exception ex)
            {
                return new ResultDto<Guid, NotesCreateStatus>(NotesCreateStatus.InternalServerError);
            }
        }



        public async Task<ResultDto<Guid, NotesUpdateStatus>> Delete(Guid id)
        {
            try
            {
                var result = await _repository.Delete(id);
                return result;
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ResultDto<Guid, NotesUpdateStatus>(NotesUpdateStatus.InternalServerError);
            }
            catch (Exception ex)
            {
                return new ResultDto<Guid, NotesUpdateStatus>(NotesUpdateStatus.InternalServerError);
            }
        }

        public async Task<NotesDto> Get(Guid id)
        {
            var result = await _repository.Get(id);
            if (result == null) return null;

            var mapperActivities = _mapper.Map<Notes, NotesDto>(result);
            return mapperActivities;
        }

        public async Task<PagedResult<NotesDto>> GetAllParsedNotes(string externalId)
        {
            var result = await _repository.GetAllParsedNotes(externalId);

            var mapperActivities = _mapper.Map<IList<Notes>, IList<NotesDto>>(result.Items);

            return new PagedResult<NotesDto>() { Results = mapperActivities };
        }

        public async Task<PagedResult<NotesDto>> GetAll(string externalId)
        {
            var result = await _repository.GetAll(externalId);

            var mapperActivities = _mapper.Map<IList<Notes>, IList<NotesDto>>(result.Items);

            return new PagedResult<NotesDto>() { Results = mapperActivities };
        }

        public async Task<ResultDto<Guid, NotesUpdateStatus>> Update(Guid id, NotesUpdateRequest request)
        {
            var model = _mapper.Map<NotesUpdateRequest, Notes>(request);
            model.Id = id;

            try
            {
                var result = await _repository.Update(model);
                return result;
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ResultDto<Guid, NotesUpdateStatus>(NotesUpdateStatus.InternalServerError);
            }
            catch (Exception ex)
            {
                return new ResultDto<Guid, NotesUpdateStatus>(NotesUpdateStatus.InternalServerError);
            }
        }

        public async Task<ResultDto<Guid, NotesUpdateStatus>> UpdateParseStatus(Guid id, bool ignore)
        {
            var result = await _repository.Get(id);
            if (result == null) return null;
            if (ignore)
            {
                result.isParsed = false;
                result.isIgnored = true;
            }
            else
            {
                result.isParsed = true;
                result.isIgnored = false;
            }

            try
            {
                return await _repository.Update(result);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ResultDto<Guid, NotesUpdateStatus>(NotesUpdateStatus.InternalServerError);
            }
            catch (Exception ex)
            {
                return new ResultDto<Guid, NotesUpdateStatus>(NotesUpdateStatus.InternalServerError);
            }
        }

    }
}
