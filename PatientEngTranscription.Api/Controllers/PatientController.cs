using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientEngTranscription.Api.Model;
using PatientEngTranscription.Domain;
using PatientEngTranscription.Service;
using PatientEngTranscription.Service.FHIRService;

namespace PatientEngTranscription.Api
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {

        private readonly ILogger<PatientController> _logger;
        private readonly IPatientService _objControllerHelper;
        private readonly FhirPatientService _fhirPatientRequestService;

        public PatientController(IPatientService objControllerHelper, FhirPatientService fhirPatientRequestService,
           ILogger<PatientController> logger)
        {
            _objControllerHelper = objControllerHelper;
            _logger = logger;
            _fhirPatientRequestService = fhirPatientRequestService;
        }
        
        
        [HttpGet("api/PatientById/{id}")]
        public async Task<ApiResponse> Get(string id)
        {
            try
            {
                var responseModel = await _objControllerHelper.Get(id);
                return new ApiResponse(responseModel);
            }
            catch(Exception ex)
            {
                throw new ApiException(ex);
            }
        }
               
        [HttpGet("api/Patient/{email}")]
        public async Task<ApiResponse> GetByEmail(string email)
        {
            try
            {
                var responseModel = await _objControllerHelper.GetByEmail(email);
                return new ApiResponse(responseModel);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }


        [HttpGet("api/Patients")]
        public async Task<ApiResponse> GetAll()
        {
            try
            {
                var responseModel = await _objControllerHelper.GetAll();
                return new ApiResponse(responseModel);
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
        [HttpPost("api/Patient")]
        [ProducesResponseType(200, Type = typeof(int))]
        public async Task<ApiResponse> Create([FromBody] PatientCreateRequest request)
        {
            try
            {
                var FhirResult = _fhirPatientRequestService.AddPatientAsync(request);
                request.ExternalId = FhirResult.Result;
                var result = await _objControllerHelper.Create(request);
                if (result.Status == PatientCreateStatus.Success)
                {                    
                    return new ApiResponse(request.ExternalId);
                }
                return new ApiResponse(400, result.Error);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
            
        }
        
        /// <summary>
        /// Update Unit Type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("api/Patient/{id}")]
        [ProducesResponseType(204)]
        public async Task<ApiResponse> Update(Guid id, [FromBody]PatientUpdateRequest request)
        {
            try
            {
                var result = await _objControllerHelper.Update(id, request);
                if (result.Status == PatientUpdateStatus.Success)
                {
                    return new ApiResponse("The record has been updated successfully");
                }
                return new ApiResponse(404, result.Error)
                {
                     StatusCode=404,
                      Message="Record not found"
                };
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }

            
        }
        
        /// <summary>
        /// Delete Unit Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("api/Patient")]
        public async Task<ApiResponse> Delete(PatientDeleteModel model)
        {
            try
            {
                var result = await _objControllerHelper.Delete(model.externalIds);
                if (result == PatientUpdateStatus.Success)
                {
                    return new ApiResponse("The record has been deleted successfully");
                }
                return new ApiResponse(404, result);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }

        }

    }
}