using Newtonsoft.Json;
using PatientEngTranscription.Domain.MedicalEntity;
using PatientEngTranscription.Domain.Models;
using PatientEngTranscription.Shared.Configurations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientEngTranscription.Service.ClinicalNotesExtraction
{
   public  class DrugSuggestionService
    {

        private readonly RootConfiguration _rootConfiguration;
        private Rootpath _rootpath;

        public DrugSuggestionService(RootConfiguration rootConfig)
        {
            _rootConfiguration = rootConfig;
        }

        public async Task<DrugSuggestionModel> GetDrugSuggestionsAsync(MedicalConditionRequest medicalRequest)
        {
            List<MedicationSuggestionModel> demo;
            try
            {
                if (medicalRequest.MedicalConditions == null) return null;

                var drugSuggestionList = await LoadFromFile();
                if (drugSuggestionList == null) return null;

                var conditionList = medicalRequest.MedicalConditions;
                if (conditionList == null) return null;


                var medicinelist = drugSuggestionList.SelectMany(x => x.Medications).ToList();

               foreach( var symptom in conditionList)
                {
                    var item = medicinelist.Where(x => x.Symptoms.Contains(symptom.Text));
                }


                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }



        private async Task<List<DrugTreatmentModel>> LoadFromFile()
        {
            List < DrugTreatmentModel > result = null;
            // var fileName = GetFilePath("Data/MedicationCategory_sample1.json");
            var fileName = GetFilePath("Data/Symptoms_drug_suggestion.json");

            using (StreamReader r = new StreamReader(fileName))
            {
                string json = await r.ReadToEndAsync();
                result = JsonConvert.DeserializeObject<List<DrugTreatmentModel>>(json);
            }
            return result;
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
