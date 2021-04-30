using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatientEngTranscription.Domain;
using PatientEngTranscription.Domain.Models;
using PatientEngTranscription.DomainLogic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientEngTranscription.Service
{
    public class MedicationFollowupService
    {

        private readonly MedicationFolloupRepository _repository;
        private readonly IMapper _mapper;

        public MedicationFollowupService(MedicationFolloupRepository medicationFolloupRepository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = medicationFolloupRepository;

        }

        public async Task<ResultDto<Guid, MedicationCreateStatus>> Create(MedicationFollowupCreateRequest request, string externalId, Guid MedicationId)
        {

            MedicationFollowUp medicationFollowup = new MedicationFollowUp
            {
                Id = Guid.NewGuid(),
                MedicationId = MedicationId,
                Status = (int)request.StatusType,
                TakenDate = request.TakenDate,
                TakenTime = request.TakenTime
            };
            var result = await _repository.Create(medicationFollowup);
            return result;
        }

        public async Task<PagedResult<MedicationFollowupDto>> GetMedicationFollowupsByMedicineId(Guid medicationId)
        {

            var result = await _repository.GetByMedicationId(medicationId);
            var mapperActivities = _mapper.Map<IList<MedicationFollowUp>, IList<MedicationFollowupDto>>(result.Items);

            return new PagedResult<MedicationFollowupDto>() { Results = mapperActivities };

        }

        public async Task<MedicationFollowupDto> GetMedicationFollowup(Guid id)
        {
            var result = await _repository.Get(id);
            var mapperActivities = _mapper.Map<MedicationFollowUp, MedicationFollowupDto>(result);
            return mapperActivities;
        }

        public async Task<ResultDto<Guid, MedicationCreateStatus>> Create(MedicationFollowupCreateRequest request)
        {
            try
            {
                var medicationfollowup = new MedicationFollowUp
                {
                    Id = Guid.NewGuid(),
                    MedicationId = request.MedicationId,
                    Status = (int)request.StatusType,
                    TakenDate = request.TakenDate,
                    TakenTime = request.TakenTime
                   
                };

                var result = await _repository.Create(medicationfollowup);
                return result;
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ResultDto<Guid, MedicationCreateStatus>(MedicationCreateStatus.InternalServerError);
            }
            catch (Exception ex)
            {
                return new ResultDto<Guid, MedicationCreateStatus>(MedicationCreateStatus.InternalServerError);
            }
        }


    public async Task<ResultDto<Guid, MedicationUpdateStatus>> Delete(Guid id)
    {
        try
        {
            var result = await _repository.Delete(id);
            return result;
        }
        catch (DbUpdateConcurrencyException)
        {
            return new ResultDto<Guid, MedicationUpdateStatus>(MedicationUpdateStatus.InternalServerError);
        }
        catch (Exception ex)
        {
            return new ResultDto<Guid, MedicationUpdateStatus>(MedicationUpdateStatus.InternalServerError);
        }
    }


}
}
