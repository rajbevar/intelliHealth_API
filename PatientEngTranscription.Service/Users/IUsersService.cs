using PatientEngTranscription.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.Service
{
    public interface IUsersService
    {
        Task<UsersDto> Get(Guid id);
        Task<PagedResult<UsersDto>> GetAll();
        Task<ResultDto<Guid, UsersCreateStatus>> Create(UsersCreateRequest request);
        Task<ResultDto<Guid, UsersUpdateStatus>> Update(Guid id, UsersUpdateRequest request);
        Task<ResultDto<Guid, UsersUpdateStatus>> Delete(Guid id);
    }
}
