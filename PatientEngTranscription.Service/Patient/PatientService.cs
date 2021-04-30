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
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        private readonly IMapper _mapper;

        public PatientService(IPatientRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResultDto<Guid, PatientCreateStatus>> Create(PatientCreateRequest request)
        {
            var model = _mapper.Map<PatientCreateRequest, Patient>(request);

            try
            {
                var result = await _repository.Create(model);
                return result;
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ResultDto<Guid, PatientCreateStatus>(PatientCreateStatus.InternalServerError);
            }
            catch (Exception ex)
            {
                return new ResultDto<Guid, PatientCreateStatus>(PatientCreateStatus.InternalServerError);
            }
        }



        public async Task<PatientUpdateStatus> Delete(IList<string> ids)
        {
            try
            {
                var result = await _repository.Delete(ids);
                return result;
            }
            catch (DbUpdateConcurrencyException)
            {
                return PatientUpdateStatus.InternalServerError;
            }
            catch (Exception ex)
            {
                return PatientUpdateStatus.InternalServerError;
            }
        }

        public async Task<PatientDto> Get(string id)
        {
            var result = await _repository.Get(id);
            if (result == null) return null;

            var mapperActivities = _mapper.Map<Patient, PatientDto>(result);
            return mapperActivities;
        }
        public async Task<PatientDto> GetByEmail(string email)
        {
            var result = await _repository.GetByEmail(email);
            if (result == null) return null;

            var mapperActivities = _mapper.Map<Patient, PatientDto>(result);
            return mapperActivities;
        }

        public async Task<PagedResult<PatientDto>> GetAll()
        {
            var result = await _repository.GetAll();

            var mapperActivities = _mapper.Map<IList<Patient>, IList<PatientDto>>(result.Items);

            return new PagedResult<PatientDto>() { Results = mapperActivities };
        }

        public async Task<ResultDto<Guid, PatientUpdateStatus>> Update(Guid id, PatientUpdateRequest request)
        {
            var model = _mapper.Map<PatientUpdateRequest, Patient>(request);
            model.Id = id;

            try
            {
                var result = await _repository.Update(model);
                return result;
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ResultDto<Guid, PatientUpdateStatus>(PatientUpdateStatus.InternalServerError);
            }
            catch (Exception ex)
            {
                return new ResultDto<Guid, PatientUpdateStatus>(PatientUpdateStatus.InternalServerError);
            }
        }
    }
}
