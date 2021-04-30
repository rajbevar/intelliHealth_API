using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatientEngTranscription.Domain;
using PatientEngTranscription.Domain.DbEntities;
using PatientEngTranscription.Domain.Models.Problems;
using PatientEngTranscription.DomainLogic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.Service
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository _repository;

        private readonly IMapper _mapper;

        public ProblemService(IProblemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResultDto<Guid, PatientCreateStatus>> Create(ProblemCreateRequest request,string externalId)
        {

            try
            {
                var model = _mapper.Map<ProblemCreateRequest, Problems>(request);

                try
                {
                    model.RecordedDate = DateTime.UtcNow;
                    model.ExternalId = externalId;
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
            catch (Exception ex)
            {
                return new ResultDto<Guid, PatientCreateStatus>(PatientCreateStatus.InternalServerError);
            }

        }       

        public async Task<PagedResult<ProblemDto>> GetAll(string id,int category)
        {
            var result = await _repository.GetAll(id,category);

            var mapperActivities = _mapper.Map<IList<Problems>, IList<ProblemDto>>(result.Items);

            return new PagedResult<ProblemDto>() { Results = mapperActivities };
        }

       
    }
}
