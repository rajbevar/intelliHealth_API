using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using PatientEngTranscription.Domain;
using PatientEngTranscription.Domain.MedicalEntity;
using PatientEngTranscription.Domain.Models;
using PatientEngTranscription.Shared.Configurations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PatientEngTranscription.Service
{
    public class DrugSuggestionAIService
    {

        private readonly IPatientService _patientService;
        private readonly IProblemService _problemService;
         private readonly RootConfiguration _rootConfiguration;
        private Rootpath _rootpath;

        
        public DrugSuggestionAIService(IPatientService patientService, IProblemService problemService, 
            IHostingEnvironment env, RootConfiguration rootConfiguration)
        {
            _patientService = patientService;
            _problemService = problemService;
            _rootConfiguration = rootConfiguration;

            _rootpath = _rootpath ?? new Rootpath();
            _rootpath.ContentRootPath = env.ContentRootPath;
            _rootpath.WebRootPath = env.WebRootPath;
        }

        public async Task<List<string>> GetMedicineSuggestionByPatientSymptoms(string externalPatientId)
        {
            var modelResult = new DrugSuggestionInputModel();
            try
            {
                //get patient demographic information
                var patientInfo = await _patientService.Get(externalPatientId);
                var patientProblems = await _problemService.GetAll(externalPatientId,1);
                if (patientProblems.Count > 0)
                {
                    foreach (var problem in patientProblems.Results)
                    {
                        if (problem.Name.Equals("fever", StringComparison.OrdinalIgnoreCase))
                        {
                            modelResult.Fever = true;
                        }

                        if (problem.Name.Equals("vomiting", StringComparison.OrdinalIgnoreCase) || problem.Name.Equals("vomite", StringComparison.OrdinalIgnoreCase))
                        {
                            modelResult.Vomiting = true;
                        }

                        if (problem.Name.Equals("headache", StringComparison.OrdinalIgnoreCase) ||
                            problem.Name.Equals("head ache", StringComparison.OrdinalIgnoreCase))
                        {
                            modelResult.Headache = true;
                        }

                        if (problem.Name.Equals("diarrhea", StringComparison.OrdinalIgnoreCase))
                        {
                            modelResult.Diarrhea = true;
                        }
                    }
                }
                
                modelResult.Age = patientInfo?.Age;
                modelResult.Weight = patientInfo?.Weight;

               
                if(_rootConfiguration.AWSAIServiceEnabled)
                {
                    var response = await InvokeAIServiceAsync(modelResult);
                    if (response.IsSuccess != true) return null;

                    //convert to medicine list
                    var (medicineList1, rawList1) = GetMedicineList(response.RawResult);
                    return rawList1;
                }
                else
                {
                    var tempdata = await LoadSampleResponseFromFile();
                    var (medicineList, rawList) = GetMedicineList(tempdata);
                    return rawList;
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<List<MedicationEntity>> GetMedicineSuggestions(string externalPatientId, MedicalEntityResponse entityResponse)
        {
            var responseEntity = new MedicalEntityResponse();
            try
            {
                //get patient demographic information
                var patientInfo = await _patientService.Get(externalPatientId);
                var inputAI = PrepareDrugSuggestionInput(entityResponse, patientInfo);

                if (_rootConfiguration.AWSAIServiceEnabled)
                {
                    var responseAI = await InvokeAIServiceAsync(inputAI);
                    if (responseAI.IsSuccess != true) return null;

                    //convert to medicine list
                    var (medicineList1, rawList1) = GetMedicineList(responseAI.RawResult);
                    return medicineList1;
                }
                else
                {
                    var tempdata1 = await LoadSampleResponseFromFile();
                    var (medicineList1, rawList1) = GetMedicineList(tempdata1);
                    return medicineList1;
                }

                //-----

               // var tempdata = await LoadSampleResponseFromFile();
               // //  var tempresult = JsonConvert.DeserializeObject<MLServiceResponse>(tempdata);


               // var response = await InvokeAIServiceAsync(inputAI);
               // if (response.IsSuccess != true) return null;

               // //convert to medicine list
               //// var (medicineList, rawList) = GetMedicineList(response.RawResult);
               // var (medicineList, rawList) = GetMedicineList(tempdata);
               // return medicineList;

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private string GetMockData()
        {
            return null;
        }




        public async Task<MLServiceResponse> InvokeAIServiceAsync(DrugSuggestionInputModel inputModel)
        {
            MLServiceResponse mlResponse = new MLServiceResponse();
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, List<Dictionary<string, string>>>() {
                        {
                            "input1",
                            new List<Dictionary<string, string>>(){new Dictionary<string, string>(){
                                            {
                                                "Age", inputModel.Age?.ToString()
                                            },
                                            {
                                                "Weight", inputModel.Weight?.ToString()
                                            },
                                            {
                                                "Pain", inputModel.Pain==true ? "1":"0"
                                            },
                                            {
                                                "Fever", inputModel.Fever==true ? "1":"0"
                                            },
                                            {
                                                "Vomiting", inputModel.Vomiting==true ? "1":"0"
                                            },
                                            {
                                                "Diarrhea", inputModel.Diarrhea==true ? "1":"0"
                                            },
                                            {
                                                "Headache", inputModel.Headache==true ? "1":"0"
                                            },
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };

                const string apiKey = "IRKObndmpuYuwSHXmtS0YZZhygs/cWuF7v5cgvebWzf2dTtBx4ezcm7D3xtps8j5zFjlmfQ8ovcX+yTMLYz08g=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/96f334343d5348109dd6b13b5f3c6743/services/ff04126f8bd04e21a4c81c0cc2e2141e/execute?api-version=2.0&format=swagger");

                // WARNING: The 'await' statement below can result in a deadlock
                // if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false)
                // so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)

                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    //  mlResponse = JsonConvert.DeserializeObject<MLServiceResponse>(result);
                    mlResponse.RawResult = result;
                    mlResponse.IsSuccess = true;
                    return mlResponse;
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp,
                    // which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    mlResponse.Error("Error occurred while calling ML Service on Cloud", responseContent);
                    return mlResponse;
                }
            }
        }

        private DrugSuggestionInputModel PrepareDrugSuggestionInput(MedicalEntityResponse entityResponse, PatientDto patientDto)
        {

            var modelResult = new DrugSuggestionInputModel();

            foreach (var item in entityResponse.MedicalConditions)
            {

                if (item.Text.Equals("fever", StringComparison.OrdinalIgnoreCase))
                {
                    modelResult.Fever = true;
                }

                if (item.Text.Equals("vomiting", StringComparison.OrdinalIgnoreCase) || item.Text.Equals("vomite", StringComparison.OrdinalIgnoreCase))
                {
                    modelResult.Vomiting = true;
                }

                if (item.Text.Equals("headache", StringComparison.OrdinalIgnoreCase) ||
                    item.Text.Equals("head ache", StringComparison.OrdinalIgnoreCase))
                {
                    modelResult.Headache = true;
                }

                if (item.Text.Equals("diarrhea", StringComparison.OrdinalIgnoreCase))
                {
                    modelResult.Diarrhea = true;
                }
            }

            modelResult.Age = patientDto?.Age;
            modelResult.Weight = patientDto?.Weight;

            return modelResult;
        }

        private (List<MedicationEntity>, List<String>) GetMedicineList(string result)
        {
            List<MedicationEntity> medicineList = new List<MedicationEntity>();
            List<String> MedicineRawList = new List<string>();
            try
            {
                var tempresult = JsonConvert.DeserializeObject<MLServiceResponse>(result);
                var strText = string.Empty;
                foreach (var item in tempresult.Results.output1)
                {
                    strText += item.value;
                }                    
                if (string.IsNullOrEmpty(strText)) return (medicineList, null);

                var splitList = strText.Split(";").ToList();
                if (splitList == null) return (medicineList, null);

                foreach (var item in splitList)
                {
                    MedicineRawList.Add(item);
                    var drugItem = GetMedication(item);
                    medicineList.Add(drugItem);
                }

                return (medicineList, MedicineRawList);

            }
            catch (Exception ex)
            {
                return (medicineList, null);
            }

        }

        private MedicationEntity GetMedication(string value)
        {
            var item = new MedicationEntity();

            var splitMedicine = value.Split(" ", 2);
            if (splitMedicine.Count() >= 2)
            {
                item.Text = splitMedicine[0];
                item.Dosage = splitMedicine[1];
            }
            else if (splitMedicine.Count() >= 1)
            {
                item.Text = splitMedicine[0];
            }
            return item;
        }

        private async Task<string> LoadSampleResponseFromFile()
        {
            string json;
            // var fileName = GetFilePath("Data/MedicationCategory_sample1.json");
            var fileName = GetFilePath("Data/AzureStudioSample.json");

            using (StreamReader r = new StreamReader(fileName))
            {
                json = await r.ReadToEndAsync();
            }
            return json;
        }

        private string GetFilePath(string fileName)
        {
            var path = Path.Combine(_rootpath.ContentRootPath, fileName);

            if (File.Exists(path))
                return path;

            path = Path.Combine(_rootpath?.WebRootPath, fileName);

            if (File.Exists(path))
                return path;

            throw new Exception("entity file is not found");

        }

    }


}
