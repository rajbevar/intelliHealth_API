using Microsoft.EntityFrameworkCore;
using PatientEngTranscription.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.DomainLogic
{
  public   class MedicationFolloupRepository
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<MedicationFollowUp> _repository;

        public MedicationFolloupRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<MedicationFollowUp>();
        }

        public async Task<MedicationFollowUp> Get(Guid id)
        {
            var result = await _repository.GetFirstOrDefaultAsync(x => x.Id ==  id, null, null, false);
            return result;
        }

        public async Task<IPagedList<MedicationFollowUp>> GetByMedicationId(Guid medicationId)
        {
            var result = await _repository.GetPagedListAsync((x => x.MedicationId == medicationId), null, null, 0, 1000, false);
            return result;
        }

        public async Task<ResultDto<Guid, MedicationCreateStatus>> Create(MedicationFollowUp medicationFollowup)
        {
            _repository.Insert(medicationFollowup);
            await _unitOfWork.SaveChangesAsync();
            return new ResultDto<Guid, MedicationCreateStatus>(MedicationCreateStatus.Success, medicationFollowup.Id);
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





    }
}
