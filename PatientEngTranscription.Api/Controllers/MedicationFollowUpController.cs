using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientEngTranscription.Domain;
using PatientEngTranscription.Domain.Models;
using PatientEngTranscription.Service;

namespace PatientEngTranscription.Api.Controllers
{
    public class MedicationFollowUpController : ControllerBase
    {

        private readonly ILogger<MedicationFollowUpController> _logger;
        private readonly IMedicationService _objControllerHelper;
        private readonly MedicationFollowupService _medicationFollowupService;

        public MedicationFollowUpController(IMedicationService objControllerHelper,
           ILogger<MedicationFollowUpController> logger, MedicationFollowupService medicationFollowupService)
        {
            _objControllerHelper = objControllerHelper;
            _logger = logger;
            _medicationFollowupService = medicationFollowupService;
        }

        [HttpGet("api/medication/{medicationId}/medication-followup")]
        public async Task<ApiResponse> GetByMedicationId(Guid medicationId)
        {
            try
            {
                var responseModel = await _medicationFollowupService.GetMedicationFollowupsByMedicineId(medicationId);
                return new ApiResponse(responseModel, 200);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        [HttpGet("api/medication-followup/{id}")]
        public async Task<ApiResponse> Get(Guid id)
        {
            try
            {
                var responseModel = await _medicationFollowupService.GetMedicationFollowup(id);
                return new ApiResponse(responseModel, 200);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        [HttpPost("api/medication-followup")]
        [ProducesResponseType(200, Type = typeof(int))]
        public async Task<ApiResponse> Create([FromBody] MedicationFollowupCreateRequest request)
        {
            try
            {
                var response = await _medicationFollowupService.Create(request);
                if(response.Status== MedicationCreateStatus.Success)
                {
                    return new ApiResponse(response);
                }
                return new ApiResponse(400, response.Error);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }

        }




    }
}
