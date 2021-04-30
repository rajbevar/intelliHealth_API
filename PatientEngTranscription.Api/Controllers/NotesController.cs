using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PatientEngTranscription.Api.HubConfig;
using PatientEngTranscription.Api.Model;
using PatientEngTranscription.Domain;
using PatientEngTranscription.Service;

namespace PatientEngTranscription.Api
{

    [ApiController]
    public class NotesController : ControllerBase
    {

        private readonly ILogger<NotesController> _logger;
        private readonly INotesService _objControllerHelper;
        private  readonly HttpClient client = new HttpClient();
        private IHubContext<ChartHub> _hub;

        public NotesController(INotesService objControllerHelper,
           ILogger<NotesController> logger, IHubContext<ChartHub> hub)
        {
            _objControllerHelper = objControllerHelper;
            _logger = logger;
            _hub = hub;
        }

        [HttpGet("api/Note/{id}")]
        public async Task<ApiResponse> Get(Guid id)
        {
            try
            {
                var responseModel = await _objControllerHelper.Get(id);
                return new ApiResponse(responseModel);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }



        [HttpGet("api/Notes")]
        public async Task<ApiResponse> GetAll(string externalId)
        {
            try
            {
                var responseModel = await _objControllerHelper.GetAll(externalId);
                return new ApiResponse(responseModel);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        [HttpGet("api/ParsedNotes")]
        public async Task<ApiResponse> ParsedNotes(string externalId)
        {
            try
            {
                var responseModel = await _objControllerHelper.GetAllParsedNotes(externalId);
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
        [HttpPost("api/Note")]
        [ProducesResponseType(200, Type = typeof(int))]
        public async Task<ApiResponse> Create([FromBody] NotesCreateRequest request)
        {
            try
            {
                var result = await _objControllerHelper.Create(request);
                if (result.Status == NotesCreateStatus.Success)
                {
                     await _hub.Clients.All.SendAsync("newnotesadded", "new notes added");

                    //var values = new SignalRRequest()
                    //{
                    //    isNew = true
                    //};

                    //var data = new StringContent(values.ToString(), Encoding.UTF8, "application/json");

                    //await client.PostAsync("https://patientliveagent.azurewebsites.net/api/AddNewNote?code=h05AWyBIoMxdceyOyVpmMaFIdCbben/4mNGjIEk2T04YYju3eqsgog==", data);
                    ////await client.PostAsync("http://localhost:7071/api/AddNewNote?code=h05AWyBIoMxdceyOyVpmMaFIdCbben/4mNGjIEk2T04YYju3eqsgog==", data);



                    return new ApiResponse(result);
                }
                return new ApiResponse(400, result.Error.Message);
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
        [HttpPut("api/Note/{id}")]
        [ProducesResponseType(204)]
        public async Task<ApiResponse> Update(Guid id, [FromBody]NotesUpdateRequest request)
        {
            try
            {
                var result = await _objControllerHelper.Update(id, request);
                if (result.Status == NotesUpdateStatus.Success)
                {
                    return new ApiResponse(result);
                }
                return new ApiResponse(400, result.Error.Message);
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
        [HttpDelete("api/Note/{id}")]
        public async Task<ApiResponse> Delete(Guid id)
        {
            try
            {
                var result = await _objControllerHelper.Delete(id);
                if (result.Status == NotesUpdateStatus.Success)
                {
                    return new ApiResponse(result);
                }
                return new ApiResponse(400, result.Error.Message);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }

        }



        [HttpPut("api/UpdateNotesParser/{NoteId}")]
        [ProducesResponseType(204)]
        public async Task<ApiResponse> SendTestToParse(Guid NoteId)
        {
            try
            {
                //var parseResult = new List<ParsedNotesModel>();

                //parseResult.Add(new ParsedNotesModel()
                //{
                //    Name = "Dolo",
                //    Doage = "500",
                //    isBot = false,
                //    Unit = "mg",
                //    Duration = "2",
                //    DurationUnit = "In a day"

                //}
                //);
                var result = await _objControllerHelper.UpdateParseStatus(NoteId, false);
                if (result.Status == NotesUpdateStatus.Success)
                {
                    return new ApiResponse(result);
                }
                return new ApiResponse(400, result.Error.Message);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }

        }

        [HttpPut("api/IgnoreNotes/{NoteId}")]
        [ProducesResponseType(204)]
        public async Task<ApiResponse> IgnoreNotes(Guid NoteId)
        {
            try
            {
                var result = await _objControllerHelper.UpdateParseStatus(NoteId, true);
                if (result.Status == NotesUpdateStatus.Success)
                {
                    return new ApiResponse(result);
                }
                return new ApiResponse(400, result.Error.Message);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }
    }

    public class SignalRRequest
    {
        public bool isNew { get; set; }
    }
}