using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientEngTranscription.Service;
using AutoWrapper.Wrappers;
using PatientEngTranscription.Domain.Models.Problems;
using PatientEngTranscription.Domain;

namespace PatientEngTranscription.Api.Controllers
{
    [ApiController]
    public class ProblemController : Controller
    {
        private readonly ILogger<ProblemController> _logger;
        private readonly IProblemService _objControllerHelper;

        public ProblemController(IProblemService objControllerHelper,
           ILogger<ProblemController> logger)
        {
            _objControllerHelper = objControllerHelper;
            _logger = logger;
        }

        [HttpGet("api/Problems")]
        public async Task<ApiResponse> GetAll(string id,int category)
        {
            try
            {
                var responseModel = await _objControllerHelper.GetAll(id,category);
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
        [HttpPost("api/Problems")]
        [ProducesResponseType(200, Type = typeof(int))]
        public async Task<ApiResponse> Create(string externalId,[FromBody] List<ProblemCreateRequest> request)
        {
            foreach (var medication in request)
            {
                var result = await _objControllerHelper.Create(medication, externalId);
                if (result.Status != PatientCreateStatus.Success)
                {
                    //return new ApiResponse(result, 200);

                    return new ApiResponse(400, result.Error);
                }
                
            }
            return new ApiResponse(200);

        }



    }
}