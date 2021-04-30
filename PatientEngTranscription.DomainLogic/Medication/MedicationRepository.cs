using Microsoft.EntityFrameworkCore;
using PatientEngTranscription.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.DomainLogic
{
    public class MedicationRepository : IMedicationRepository
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Medication> _repository;

        public MedicationRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Medication>();
        }

        public async Task<ResultDto<Guid, MedicationCreateStatus>> Create(Medication request)
        {
            //var checkAlreadyExist = await Get(request.Code);
            //if (checkAlreadyExist != null)
            //{
            //    return new ResultDto<Guid, MedicationCreateStatus>(MedicationCreateStatus.AlreadyExist);
            //}
           
            _repository.Insert(request);
            await _unitOfWork.SaveChangesAsync();
            return new ResultDto<Guid, MedicationCreateStatus>(MedicationCreateStatus.Success, request.Id);
        }

        public async Task<ResultDto<Guid, MedicationUpdateStatus>> Delete(Guid id)
        {
            var existingObjectResult = await Get(id);
            if (existingObjectResult == null)
            {
                return new ResultDto<Guid, MedicationUpdateStatus>(MedicationUpdateStatus.NotFound, id);
            }
            
            _repository.Delete(existingObjectResult);
           
            await _unitOfWork.SaveChangesAsync();
            return new ResultDto<Guid, MedicationUpdateStatus>(MedicationUpdateStatus.Success, id);
        }

        public async Task<Medication> Get(Guid id)
        {
            var result = await _repository.GetFirstOrDefaultAsync(x => x.Id == id, null, null, false);

            return result;
        }

        public async Task<Medication> Get(string code)
        {
            var result = await _repository.GetFirstOrDefaultAsync(x => x.Code.ToLower() == code.ToLower(), null, null, false);

            return result;
        }

        public async Task<IPagedList<Medication>> GetAll(string externalId)
        {
            //var result = await _repository.GetPagedListAsync();
            var result = await _repository.GetPagedListAsync((x => x.ExternalId == externalId), null, null, 0, 20, false);


            return result;
        }

        public async Task<ResultDto<Guid, MedicationUpdateStatus>> Update(Medication request)
        {
            var checkAlreadyExist = await Get(request.Code);
            if (checkAlreadyExist != null)
            {
                if (checkAlreadyExist.Id != request.Id)
                {
                    return new ResultDto<Guid, MedicationUpdateStatus>(MedicationUpdateStatus.AlreadyExist);
                }
            }
            var existingObjectResult = await Get(request.Id);
            if (existingObjectResult == null)
            {
                return new ResultDto<Guid, MedicationUpdateStatus>(MedicationUpdateStatus.NotFound, request.Id);
            }
         
            existingObjectResult.Doage = request.Doage;
            existingObjectResult.Frenquency = request.Frenquency;
            existingObjectResult.Code = request.Code;
          

            _repository.Update(existingObjectResult);
            await _unitOfWork.SaveChangesAsync();
            return new ResultDto<Guid, MedicationUpdateStatus>(MedicationUpdateStatus.Success, request.Id);
        }
    }
}
