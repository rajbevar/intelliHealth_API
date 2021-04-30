using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatientEngTranscription.Domain;
using PatientEngTranscription.Domain.MedicalEntity;
using PatientEngTranscription.Domain.Models;
using PatientEngTranscription.DomainLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientEngTranscription.Service
{
    public class MedicationService : IMedicationService
    {
        private readonly IMedicationRepository _repository;
        private readonly MedicationFollowupService _medicationFollowupService;

        private readonly IMapper _mapper;

        public MedicationService(IMedicationRepository repository, IMapper mapper, MedicationFollowupService medicationFollowupService)
        {
            _repository = repository;
            _mapper = mapper;
            _medicationFollowupService = medicationFollowupService;
        }

        public async Task<ResultDto<Guid, MedicationCreateStatus>> Create(MedicationEntity request, string externalId)
        {

            try
            {
                var model = _mapper.Map<MedicationEntity, MedicationCreateRequest>(request);

                try
                {

                    var _model = _mapper.Map<MedicationCreateRequest, Medication>(model);
                    _model.ExternalId = externalId; 
                    _model.EffectiveDate = DateTime.UtcNow;

                    //convert frequency string to numbers
                    var (frequncyNum, frequncyTimings) = MedicationHelper.GetMedicationFrequentyInNumber(_model.Frenquency);
                    _model.FrenquencyInDay = frequncyNum;
                    var result = await _repository.Create(_model);
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

        public async Task<MedicationDto> Get(Guid id)
        {
            var result = await _repository.Get(id);
            if (result == null) return null;

            var mapperActivities = _mapper.Map<Medication, MedicationDto>(result);
            return mapperActivities;
        }

        public async Task<PagedResult<MedicationDto>> GetAll(string externalId)
        {
            var result = await _repository.GetAll(externalId);

            var mapperActivities = _mapper.Map<IList<Medication>, IList<MedicationDto>>(result.Items);

            return new PagedResult<MedicationDto>() { Results = mapperActivities };
        }

        public async Task<ResultDto<Guid, MedicationUpdateStatus>> Update(Guid id, MedicationUpdateRequest request)
        {
            var model = _mapper.Map<MedicationUpdateRequest, Medication>(request);
            model.Id = id;

            try
            {
                var result = await _repository.Update(model);
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

        public async Task<PagedResult<MedicationDto>> GetWithFollowup(string externalId)
        {
            var result = await _repository.GetAll(externalId);
            var medicationResult = _mapper.Map<IList<Medication>, IList<MedicationDto>>(result.Items);

            if (result.TotalCount <= 0)
            {
                return new PagedResult<MedicationDto>() { Results = medicationResult };
            }

            //  var response= new PagedResult<MedicationDto>() { Results = medicationResult };


            foreach (var medicationItem in medicationResult)
            {
                var followupResult = await _medicationFollowupService.GetMedicationFollowupsByMedicineId(medicationItem.Id);
                if (followupResult.Count == 0)
                {
                    medicationItem.MedicationFollowUps = GetDefaultFollowupWithTimings(medicationItem)?.ToList();
                }
                else
                {
                    var filterRecords = GetFormattedFollowup(followupResult.Results, medicationItem);
                    medicationItem.MedicationFollowUps = filterRecords;
                }
               
            }
            return new PagedResult<MedicationDto>() { Results = medicationResult };
        }

        private List<MedicationFollowupDto> GetFormattedFollowup(IList<MedicationFollowupDto> followups, MedicationDto medication)
        {
            List<MedicationFollowupDto> medicationFollowupList = new List<MedicationFollowupDto>();
            if (medication.Duration <= 0) return medicationFollowupList;

            //generate all records based on duration
            var defaultFollowUpList = GetDefaultFollowupWithTimings(medication, followups);

            return defaultFollowUpList;

        }

        private List<MedicationFollowupDto> GetDefaultFollowup(MedicationDto medication, IList<MedicationFollowupDto> followups = null)
        {
            List<MedicationFollowupDto> medicationFollowupList = new List<MedicationFollowupDto>();

            var startDate = medication.EffectiveDate.Value;
            var endDate = startDate.AddDays(medication.Duration);

            for (int i = 0; i < medication.Duration; i++)
            {
                MedicationFollowupDto result = new MedicationFollowupDto();
                result.MedicationId = medication.Id;
                result.Status = (int)MedicationFollowupStatus.NOTTAKEN;
                result.TakenDate = startDate.AddDays(i);
                result.TakenTime = string.Empty;



                if (followups != null)
                {

                    var originalRecord = followups.Where(x => x.TakenDate.Day == result.TakenDate.Day && x.TakenDate.Month == result.TakenDate.Month
                    && x.TakenDate.Year == result.TakenDate.Year).ToList();
                    if (originalRecord.Any())
                    {
                        medicationFollowupList.AddRange(originalRecord);
                    }
                    else
                    {
                        medicationFollowupList.Add(result);
                    }
                }
                else
                {
                    medicationFollowupList.Add(result);
                }

            }
            return medicationFollowupList;
        }
        private List<MedicationFollowupDto> GetDefaultFollowupWithTimings(MedicationDto medication, IList<MedicationFollowupDto> followups = null)
        {
            List<MedicationFollowupDto> medicationFollowupList = new List<MedicationFollowupDto>();

            if (medication.EffectiveDate == null) return null;
            var startDate = medication.EffectiveDate.GetValueOrDefault();
            var endDate = startDate.AddDays(medication.Duration);

            for (int i = 0; i < medication.Duration; i++)
            {
                MedicationFollowupDto result = new MedicationFollowupDto();
                result.MedicationId = medication.Id;
                result.Status = (int)MedicationFollowupStatus.NOTTAKEN;
                result.TakenDate = startDate.AddDays(i);
                result.TakenTime = string.Empty;

                //Get timings


                if (followups != null)
                {

                    var originalRecord = followups.Where(x => x.TakenDate.Day == result.TakenDate.Day && x.TakenDate.Month == result.TakenDate.Month
                    && x.TakenDate.Year == result.TakenDate.Year).ToList();
                    if (originalRecord.Any())
                    {
                        //  medicationFollowupList.AddRange(originalRecord);
                        var filterItems = GetTimings(medication, result, originalRecord);
                        medicationFollowupList.AddRange(filterItems);
                    }
                    else
                    {
                        // medicationFollowupList.Add(result);
                        var filterItems = GetTimings(medication, result, null);
                        medicationFollowupList.AddRange(filterItems);
                    }
                }
                else
                {
                 //   medicationFollowupList.Add(result);
                    var filterItems = GetTimings(medication, result, null);
                    medicationFollowupList.AddRange(filterItems);
                }

            }
            return medicationFollowupList;
        }


        private List<MedicationFollowupDto> GetTimings(MedicationDto medication, MedicationFollowupDto followup, IList<MedicationFollowupDto> followups = null)
        {
            var timingRecords = new List<MedicationFollowupDto>();
            var (intTimings, timingList) = MedicationHelper.GetMedicationFrequentyInNumber(medication.Frenquency);

            if (timingList == null)
            {
                timingRecords.Add(followup);
            }

            foreach (var timingItem in timingList)
            {
                var item = new MedicationFollowupDto
                {
                    MedicationId = medication.Id,
                    //  Status = (int)MedicationFollowupStatus.NOTTAKEN,
                    TakenDate = followup.TakenDate,
                    TakenTime = timingItem

                };
                var takenRecords = followups?.Where(x => x.TakenTime == timingItem).FirstOrDefault();
                if (takenRecords != null)
                {
                    timingRecords.Add(takenRecords);
                }
                else
                {
                    item.Status = (int)MedicationFollowupStatus.NOTTAKEN;
                    timingRecords.Add(item);
                }
            }
            return timingRecords;

        }
    }
}
