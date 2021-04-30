using Microsoft.EntityFrameworkCore;
using PatientEngTranscription.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.DomainLogic
{
    public class PatientRepository : IPatientRepository
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Patient> _repository;

        public PatientRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Patient>();
        }

        public async Task<ResultDto<Guid, PatientCreateStatus>> Create(Patient request)
        {
            var checkAlreadyExist = await Get(request.Email);
            if (checkAlreadyExist != null)
            {
                return new ResultDto<Guid, PatientCreateStatus>(PatientCreateStatus.AlreadyExist);
            }
            request.IsDeleted = false;
            _repository.Insert(request);
            await _unitOfWork.SaveChangesAsync();
            return new ResultDto<Guid, PatientCreateStatus>(PatientCreateStatus.Success, request.Id);
        }

        public async Task<PatientUpdateStatus> Delete(IList<string> ids)
        {
            foreach(var id in ids)
            {
                var existingObjectResult = await Get(id);
                if (existingObjectResult == null)
                {
                    return PatientUpdateStatus.NotFound;
                }
                _repository.Delete(existingObjectResult);
            }
            
            // existingObjectResult.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
            return PatientUpdateStatus.Success;
        }

        public async Task<Patient> Get(string id)
        {
            var result = await _repository.GetFirstOrDefaultAsync(x => x.ExternalId == id, null, null, false);

            return result;
        }
        

        public async Task<Patient> GetByEmail(string email)
        {
            var result = await _repository.GetFirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower(), null, null, false);

            return result;
        }

        public async Task<IPagedList<Patient>> GetAll()
        {
            var result = await _repository.GetPagedListAsync(null,null,null,0,100,false);

            return result;
        }

        public async Task<ResultDto<Guid, PatientUpdateStatus>> Update(Patient request)
        {
            var checkAlreadyExist = await GetByEmail(request.Email);
            if (checkAlreadyExist != null)
            {
                if (checkAlreadyExist.Id != request.Id)
                {
                    return new ResultDto<Guid, PatientUpdateStatus>(PatientUpdateStatus.AlreadyExist);
                }
            }
            var existingObjectResult = await Get(request.ExternalId);
            if (existingObjectResult == null)
            {
                return new ResultDto<Guid, PatientUpdateStatus>(PatientUpdateStatus.NotFound, request.Id);
            }
            existingObjectResult.IsDeleted = false;
            existingObjectResult.Firstname = request.Firstname;
            existingObjectResult.Lastname = request.Lastname;
            existingObjectResult.DateOfBirth = request.DateOfBirth;
            existingObjectResult.PhoneNumber = request.PhoneNumber;

            _repository.Update(existingObjectResult);
            await _unitOfWork.SaveChangesAsync();
            return new ResultDto<Guid, PatientUpdateStatus>(PatientUpdateStatus.Success, request.Id);
        }
    }
}
