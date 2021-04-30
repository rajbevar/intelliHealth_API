using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PatientEngTranscription.Domain;

namespace PatientEngTranscription.DomainLogic
{
    public interface IUsersRepository
    {
        Task<Users> Get(Guid id);
        Task<IPagedList<Users>> GetAll();
        Task<ResultDto<Guid, UsersCreateStatus>> Create(Users request);
        Task<ResultDto<Guid, UsersUpdateStatus>> Update(Users request);
        Task<ResultDto<Guid, UsersUpdateStatus>> Delete(Guid id);
    }
}
