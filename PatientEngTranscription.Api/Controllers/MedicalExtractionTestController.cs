using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using PatientEngTranscription.Service;
using PatientEngTranscription.Service.ClinicalNotesExtraction;
using System;
using System.Threading.Tasks;

namespace PatientEngTranscription.Api.Controllers
{
    public class MedicalExtractionTestController : ControllerBase
    {

        private readonly ClinicalNotesExtractionService _extrctService;
        private readonly DrugSuggestionAIService _drugSuggestionAIService;

        public MedicalExtractionTestController(ClinicalNotesExtractionService extrctService, DrugSuggestionAIService drugSuggestionAIService)
        {
            _extrctService = extrctService;
            _drugSuggestionAIService = drugSuggestionAIService;
        }

        //[HttpGet("api/Medical/entities/extraction")]
        //public async Task<ApiResponse> Get()
        //{
        //    try
        //    {
        //        var responseModel = await _extrctService.ExtractMedicalEntitiesAsync(string.Empty);
        //        responseModel.SuggestedMedications = await _drugSuggestionAIService.GetMedicineSuggestions("2645016", responseModel);

        //        return new ApiResponse(responseModel, 200);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiException(ex);
        //    }
        //}

        [HttpPost("api/patient/{externalId}/Medical/entities/awsextraction")]
        public async Task<ApiResponse> GetExtract(string externalId, [FromBody]TestNote request)
        {
            try
            {
                //if (request.token == "54321")
                //{
                    var responseModel = await _extrctService.ExtractMedicalEntitiesAsync(request.notes);
                    //get medicine suggestion list
                    responseModel.SuggestedMedications = await _drugSuggestionAIService.GetMedicineSuggestions(externalId, responseModel);
                    return new ApiResponse(responseModel, 200);
                //}
                //return new ApiResponse(200);

            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        [HttpGet("api/patient/{externalId}/Medicals/suggestions")]
        public async Task<ApiResponse> GetMedicinesSuggestions(string externalId)
        {
            try
            {
                var responseModel = await _drugSuggestionAIService.GetMedicineSuggestionByPatientSymptoms(externalId);
                return new ApiResponse(responseModel, 200);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }
    }

    public class TestNote
    {
        public string notes { get; set; }
        //public string token { get; set; }
    }
}
