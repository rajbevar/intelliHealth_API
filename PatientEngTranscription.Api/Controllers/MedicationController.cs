using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientEngTranscription.Domain;
using PatientEngTranscription.Service;

namespace PatientEngTranscription.Api.Controllers
{
    
    [ApiController]
    public class MedicationController : ControllerBase
    {

        private readonly ILogger<MedicationController> _logger;
        private readonly IMedicationService _objControllerHelper;

        public MedicationController(IMedicationService objControllerHelper,
           ILogger<MedicationController> logger)
        {
            _objControllerHelper = objControllerHelper;
            _logger = logger;
        }

        [HttpGet("api/Medication/{id}")]
        public async Task<ApiResponse> Get(Guid id)
        {
            try
            {
                var responseModel = await _objControllerHelper.Get(id);
                return new ApiResponse(responseModel,200);
            }
            catch(Exception ex)
            {
                throw new ApiException(ex);
            }
        }



        [HttpGet("api/Medications")]
        public async Task<ApiResponse> GetAll(string externalId)
        {
            try
            {
                var responseModel = await _objControllerHelper.GetAll(externalId);
                return new ApiResponse(responseModel, 200);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        [HttpGet("api/Medications/medicationfollowup")]
        public async Task<ApiResponse> GetMedicationsWithFollowUp(string externalId)
        {
            try
            {
                var responseModel = await _objControllerHelper.GetWithFollowup(externalId);
                return new ApiResponse(responseModel, 200);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        /// <summary>
        /// Create Unit Type
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("api/Medication")]
        [ProducesResponseType(200, Type = typeof(int))]
        public async Task<ApiResponse> Create([FromBody] MedCreateRequest request)
        {
            var medEntity = request.medicationEntity;
            foreach (var medication in medEntity)
            {
                var result = await _objControllerHelper.Create(medication, request.ExternalId);
                if (result.Status != MedicationCreateStatus.Success)
                {
                    //return new ApiResponse(result, 200);
                    return new ApiResponse(400, result.Error);
                }

                //return new ApiResponse(400, result.Error);
            }
            return new ApiResponse(200);

        }


        /// <summary>
        /// Update Unit Type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("api/Medication/{id}")]
        [ProducesResponseType(204)]
        public async Task<ApiResponse> Update(Guid id, [FromBody]MedicationUpdateRequest request)
        {
            var result = await _objControllerHelper.Update(id, request);
            if (result.Status == MedicationUpdateStatus.Success)
            {
                return new ApiResponse("The record updated successfully");
            }
            return new ApiResponse(400, result.Error);
        }


        /// <summary>
        /// Delete Unit Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("api/Medication/{id}")]
        public async Task<ApiResponse> Delete(Guid id)
        {
            var result = await _objControllerHelper.Delete(id);
            if (result.Status == MedicationUpdateStatus.Success)
            {
                return new ApiResponse("The record updated successfully");
            }
            return new ApiResponse(400, result.Error);
        }
    }
}