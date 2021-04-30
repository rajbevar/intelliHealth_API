using Microsoft.EntityFrameworkCore;
using PatientEngTranscription.Domain;
using PatientEngTranscription.Domain.DbEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.DomainLogic
{
    public class ProblemRepository : IProblemRepository
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Problems> _repository;

        public ProblemRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<Problems>();
        }

        public async Task<ResultDto<Guid, PatientCreateStatus>> Create(Problems request)
        {           
            _repository.Insert(request);
            await _unitOfWork.SaveChangesAsync();
            return new ResultDto<Guid, PatientCreateStatus>(PatientCreateStatus.Success, request.Id);
        }

        
        public async Task<IPagedList<Problems>> GetAll(string externalId,int category)
        {
            
            //var result = await _repository.GetPagedListAsync();
            if (category == 1 )
                 return await _repository.GetPagedListAsync((x => x.ExternalId == externalId && x.category == Domain.Enums.ProblemCategory.Patient), null, null, 0, 20, false);

            else if (category ==2)
                return await _repository.GetPagedListAsync((x => x.ExternalId == externalId && x.category == Domain.Enums.ProblemCategory.Doctor), null, null, 0, 20, false);

            else

                 return await _repository.GetPagedListAsync((x => x.ExternalId == externalId && x.category ==  Domain.Enums.ProblemCategory.All), null, null, 0, 20, false);


            
        }

       }
}
