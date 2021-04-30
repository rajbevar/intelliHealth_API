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

namespace PatientEngTranscription.Api
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _objControllerHelper;

        public UsersController(IUsersService objControllerHelper,
           ILogger<UsersController> logger)
        {
            _objControllerHelper = objControllerHelper;
            _logger = logger;
        }

        [HttpGet("api/User/{id}")]
        public async Task<ApiResponse> Get(Guid id)
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

        [HttpGet("api/Users")]
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
        [HttpPost("api/User")]
        [ProducesResponseType(200, Type = typeof(int))]
        public async Task<ApiResponse> Create([FromBody] UsersCreateRequest request)
        {
            try
            {
                var result = await _objControllerHelper.Create(request);
                if (result.Status == UsersCreateStatus.Success)
                {
                    return new ApiResponse(result.Content);
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
        [HttpPut("api/User/{id}")]
        [ProducesResponseType(204)]
        public async Task<ApiResponse> Update(Guid id, [FromBody]UsersUpdateRequest request)
        {
            try
            {
                var result = await _objControllerHelper.Update(id, request);
                if (result.Status == UsersUpdateStatus.Success)
                {
                    return new ApiResponse("The record has been updated successfully");
                }
                return new ApiResponse(400, result.Error);
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
        [HttpDelete("api/User/{id}")]
        public async Task<ApiResponse> Delete(Guid id)
        {
            try
            {
                var result = await _objControllerHelper.Delete(id);
                if (result.Status == UsersUpdateStatus.Success)
                {
                    return new ApiResponse(result.Content);
                }
                return new ApiResponse(400, result.Error);

            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
           
        }
    }
}