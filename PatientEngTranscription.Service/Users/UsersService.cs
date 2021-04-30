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
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;

        private readonly IMapper _mapper;

        public UsersService(IUsersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResultDto<Guid, UsersCreateStatus>> Create(UsersCreateRequest request)
        {
            var model = _mapper.Map<UsersCreateRequest, Users>(request);

            try
            {
                var result = await _repository.Create(model);
                return result;
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ResultDto<Guid, UsersCreateStatus>(UsersCreateStatus.InternalServerError);
            }
            catch (Exception ex)
            {
                return new ResultDto<Guid, UsersCreateStatus>(UsersCreateStatus.InternalServerError);
            }
        }

       

        public async Task<ResultDto<Guid, UsersUpdateStatus>> Delete(Guid id)
        {
            try
            {
                var result = await _repository.Delete(id);
                return result;
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ResultDto<Guid, UsersUpdateStatus>(UsersUpdateStatus.InternalServerError);
            }
            catch (Exception ex)
            {
                return new ResultDto<Guid, UsersUpdateStatus>(UsersUpdateStatus.InternalServerError);
            }
        }

        public async Task<UsersDto> Get(Guid id)
        {
            var result = await _repository.Get(id);
            if (result == null) return null;

            var mapperActivities = _mapper.Map<Users, UsersDto>(result);
            return mapperActivities;
        }

        public async Task<PagedResult<UsersDto>> GetAll()
        {
            var result = await _repository.GetAll();

            var mapperActivities = _mapper.Map<IList<Users>, IList<UsersDto>>(result.Items);

            return new PagedResult<UsersDto>() { Results = mapperActivities };
        }

        public async Task<ResultDto<Guid, UsersUpdateStatus>> Update(Guid id, UsersUpdateRequest request)
        {
            var model = _mapper.Map<UsersUpdateRequest, Users>(request);
            model.Id = id;

            try
            {
                var result = await _repository.Update(model);
                return result;
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ResultDto<Guid, UsersUpdateStatus>(UsersUpdateStatus.InternalServerError);
            }
            catch (Exception ex)
            {
                return new ResultDto<Guid, UsersUpdateStatus>(UsersUpdateStatus.InternalServerError);
            }
        }
    }
}
