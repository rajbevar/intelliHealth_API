using Microsoft.EntityFrameworkCore;
using PatientEngTranscription.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.DomainLogic
{
    public class UsersRepository : IUsersRepository
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Users> _repository;

        public UsersRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Users>();
        }

        public async Task<ResultDto<Guid, UsersCreateStatus>> Create(Users request)
        {
            var checkAlreadyExist = await Get(request.Username);
            if (checkAlreadyExist != null)
            {
                return new ResultDto<Guid, UsersCreateStatus>(UsersCreateStatus.AlreadyExist);
            }
            request.IsDeleted = false;
            _repository.Insert(request);
            await _unitOfWork.SaveChangesAsync();
            return new ResultDto<Guid, UsersCreateStatus>(UsersCreateStatus.Success, request.Id);
        }

        public async Task<ResultDto<Guid, UsersUpdateStatus>> Delete(Guid id)
        {
            var existingObjectResult = await Get(id);
            if (existingObjectResult == null)
            {
                return new ResultDto<Guid, UsersUpdateStatus>(UsersUpdateStatus.NotFound, id);
            }
            existingObjectResult.Id = id;
             existingObjectResult.IsDeleted =false;
            _repository.Delete(existingObjectResult);
            await _unitOfWork.SaveChangesAsync();
            return new ResultDto<Guid, UsersUpdateStatus>(UsersUpdateStatus.Success, id);
        }

        public async Task<Users> Get(Guid id)
        {
            var result = await _repository.GetFirstOrDefaultAsync(x => x.Id == id, null, null, false);

            return result;
        }

        public async Task<Users> Get(string username)
        {
            var result = await _repository.GetFirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower(), null, null, false);

            return result;
        }

        public async Task<IPagedList<Users>> GetAll()
        {
            var result = await _repository.GetPagedListAsync();
            
            return result;
        }

        public async Task<ResultDto<Guid, UsersUpdateStatus>> Update(Users request)
        {
            var checkAlreadyExist = await Get(request.Username);
            if (checkAlreadyExist != null)
            {
                if (checkAlreadyExist.Id != request.Id)
                {
                    return new ResultDto<Guid, UsersUpdateStatus>(UsersUpdateStatus.AlreadyExist);
                }
            }
            var existingObjectResult = await Get(request.Id);
            if (existingObjectResult == null)
            {
                return new ResultDto<Guid, UsersUpdateStatus>(UsersUpdateStatus.NotFound, request.Id);
            }
            existingObjectResult.IsDeleted = true;
            existingObjectResult.Firstname = request.Firstname;
            existingObjectResult.Lastname = request.Lastname;
            existingObjectResult.RecogProfileId = request.RecogProfileId;
           
            _repository.Update(existingObjectResult);
            await _unitOfWork.SaveChangesAsync();
            return new ResultDto<Guid, UsersUpdateStatus>(UsersUpdateStatus.Success, request.Id);
        }

       
    }
}
