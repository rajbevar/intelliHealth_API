using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientEngTranscription.Domain;
using PatientEngTranscription.Domain.MedicalEntity;
using PatientEngTranscription.Service;
using PatientEngTranscription.Service.FHIRService;

namespace PatientEngTranscription.Api.Controllers
{
    public class FhirTestController : ControllerBase
    {
        private readonly FhirMedicalRequestService _fhirMedicalRequestService;
        private readonly  FhirConditionService  _fhirConditionService;

        public FhirTestController(FhirMedicalRequestService fhirMedicalRequestService,
            FhirConditionService fhirConditionService)
        {
            _fhirMedicalRequestService = fhirMedicalRequestService;
            _fhirConditionService = fhirConditionService;
        }

        [HttpPost("api/Fhir/patient/add-medicines")]
        public async Task<ApiResponse> AddMedicineToPatient(string emrPatientId, [FromBody] List<MedicationEntity> medicineList)
        {
            try
            {
               //emrPatientId = "1795445";
               var result = await  _fhirMedicalRequestService.AddMedicinesToPatientAsync(emrPatientId, medicineList);
               return new ApiResponse("success");
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }

        }

        [HttpPost("api/Fhir/patient/add-medical-conditions")]
        public async Task<ApiResponse> AddMedicineToPatient(string emrPatientId, [FromBody] MedicalConditionRequest request)
        {
            try
            {
                //emrPatientId = "1795445";
                var result = await _fhirConditionService.AddPatientConditions(emrPatientId, request);
                return new ApiResponse("success");
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }


    }
}
