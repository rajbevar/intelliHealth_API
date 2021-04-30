using Amazon.ComprehendMedical;
using Amazon.ComprehendMedical.Model;
using Microsoft.AspNetCore.Hosting;
using PatientEngTranscription.AWSService;
using PatientEngTranscription.Domain.MedicalEntity;
using PatientEngTranscription.Domain.Models;
using PatientEngTranscription.Shared.Configurations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PatientEngTranscription.Service.ClinicalNotesExtraction
{
    public class ClinicalNotesExtractionService
    {

        private readonly ComprehendMedicalService _comprehendMedicalService;
        private readonly ComprehendMedicalServiceMock _comprehendMedicalServiceMock;
        private readonly Rootpath _rootpath;
        private readonly RootConfiguration _rootConfiguration;

        public ClinicalNotesExtractionService(
            ComprehendMedicalService comprehendMedicalService,
            ComprehendMedicalServiceMock comprehendMedicalServiceMock, IHostingEnvironment env,
            RootConfiguration rootConfiguration)
        {
            _comprehendMedicalService = comprehendMedicalService;
            _comprehendMedicalServiceMock = comprehendMedicalServiceMock;

            _rootpath = _rootpath ?? new Rootpath();
            _rootpath.ContentRootPath = env.ContentRootPath;
            _rootpath.WebRootPath = env.WebRootPath;
            _rootConfiguration = rootConfiguration;
        }

        public async Task<MedicalEntityResponse> ExtractMedicalEntitiesAsync(string clinicalNotes)
        {
            MedicalEntityResponse response = new MedicalEntityResponse();
            DetectEntitiesV2Response result = null;
            try
            {
                if(_rootConfiguration.AWSAIServiceEnabled)
                {
                     result = await ExtractMedicalEntitiesAWS(clinicalNotes);
                }
                else
                {
                    result = await ExtractMedicalEntitiesMock(clinicalNotes);
                }
                // 
                if (result.HttpStatusCode != HttpStatusCode.OK)
                {
                    response.IsError = true;
                    response.StatusCode = (int)result.HttpStatusCode;
                    return response;
                }

                ParseEntities(result.Entities, response);

                return response;

            }
            catch (Exception)
            {

                throw;
            }

            // var result = await ExtractMedicalEntitiesAWS(clinicalNotes);
            //var result = await ExtractMedicalEntitiesMock(clinicalNotes);
            //if (result.StatusCode != 200)
            //{
            //    return result;
            //}
            ////parse the response and convert into custom entity model
            //result.Medications = result.Medications ?? new List<MedicationEntity>();
            //result.Medications = GetMeidcationEntities(result)
            //return result;

        }


        public async Task<MedicalEntityResponse> ExtractMedicalEntitiesV1Async(string externalPatientId, string clinicalNotes)
        {
            MedicalEntityResponse response = new MedicalEntityResponse();
            try
            {
              //  var result = await ExtractMedicalEntitiesAWS(clinicalNotes);
               var result = await ExtractMedicalEntitiesMock(clinicalNotes);
                if (result.HttpStatusCode != HttpStatusCode.OK)
                {
                    response.IsError = true;
                    response.StatusCode = (int)result.HttpStatusCode;
                    return response;
                }

                ParseEntities(result.Entities, response);

                return response;

            }
            catch (Exception)
            {

                throw;
            }

            // var result = await ExtractMedicalEntitiesAWS(clinicalNotes);
            //var result = await ExtractMedicalEntitiesMock(clinicalNotes);
            //if (result.StatusCode != 200)
            //{
            //    return result;
            //}
            ////parse the response and convert into custom entity model
            //result.Medications = result.Medications ?? new List<MedicationEntity>();
            //result.Medications = GetMeidcationEntities(result)
            //return result;

        }

        private async Task<DetectEntitiesV2Response> ExtractMedicalEntitiesAWS(string clinicalNotes)
        {
            var awsResponse = await _comprehendMedicalService.DetectEntities(clinicalNotes);
            return awsResponse;


        }

        private async Task<DetectEntitiesV2Response> ExtractMedicalEntitiesMock(string clinicalNotes)
        {
            var awsResponse = await _comprehendMedicalServiceMock.DetectEntities(clinicalNotes, _rootpath);
            return awsResponse;

        }

        private MedicalEntityResponse ParseEntities(List<Entity> awsEntities, MedicalEntityResponse response)
        {

            var medicationEntities = new List<MedicationEntity>();
            var medicalConditions = new List<MedicalConditionEntity>();
            var anatomyList = new List<AnatomyEntity>();

            foreach (var entity in awsEntities)
            {

                if (entity.Category == EntityType.MEDICAL_CONDITION)
                {
                    var medicacCondition = GetMedicalConditionEntity(entity);
                    medicalConditions.Add(medicacCondition);
                }
                else if (entity.Category == EntityType.MEDICATION)
                {
                    var medicalEntity = GetMedicationEntity(entity);
                    medicationEntities.Add(medicalEntity);
                }
                else if (entity.Category == EntityType.ANATOMY)
                {
                    anatomyList.Add(GetAnatomyEntity(entity));
                }
            }
            response.Medications = medicationEntities;
            response.MedicalConditions = medicalConditions;
            response.AnatomyList = anatomyList;
            return response;
        }

        private MedicationEntity GetMedicationEntity(Entity entity)
        {

            var medEntity = new MedicationEntity
            {
                BeginOffset = entity.BeginOffset,
                EndOffset = entity.EndOffset,
                Id = entity.Id,
                Type = entity.Type.ToString(),
                Category = entity.Category.ToString(),
                Text = entity.Text,
                Score = entity.Score
            };
            //extract attributes
            foreach (var attr in entity.Attributes)
            {

                if (attr.Type == EntitySubType.ROUTE_OR_MODE)
                {
                    medEntity.RouteOrMode = attr.Text;
                }
                else if (attr.Type == EntitySubType.STRENGTH)
                {
                    medEntity.Strength = attr.Text;
                }
                else if (attr.Type == EntitySubType.FORM)
                {
                    medEntity.Form = attr.Text;
                }
                else if (attr.Type == EntitySubType.FREQUENCY)
                {
                    medEntity.Frequency = attr.Text;
                }
                else if (attr.Type == EntitySubType.DURATION)
                {
                    medEntity.Duration = attr.Text;
                }
                else if (attr.Type == EntitySubType.DOSAGE)
                {
                    medEntity.Dosage = attr.Text;
                }
                else if (attr.Type == EntitySubType.RATE)
                {
                    medEntity.Rate = attr.Text;
                }

            }

            return medEntity;
        }

        private MedicalConditionEntity GetMedicalConditionEntity(Entity entity)
        {

            var medEntity = new MedicalConditionEntity
            {
                BeginOffset = entity.BeginOffset,
                EndOffset = entity.EndOffset,
                Id = entity.Id,
                Type = entity.Type.ToString(),
                Category = entity.Category.ToString(),
                Text = entity.Text,
                Score = entity.Score
            };
            //extract attributes
            foreach (var trait in entity.Traits)
            {

                if (trait.Name == AttributeName.SYMPTOM)
                {
                    medEntity.TypeName = trait.Name.ToString();
                }
                else if (trait.Name == AttributeName.SIGN)
                {
                    medEntity.TypeName = trait.Name.ToString();
                }
                else if (trait.Name == AttributeName.DIAGNOSIS)
                {
                    medEntity.TypeName = trait.Name.ToString();
                }

            }
            return medEntity;
        }

        private AnatomyEntity GetAnatomyEntity(Entity entity)
        {

            var medEntity = new AnatomyEntity
            {
                BeginOffset = entity.BeginOffset,
                EndOffset = entity.EndOffset,
                Id = entity.Id,
                Type = entity.Type.ToString(),
                Category = entity.Category.ToString(),
                Text = entity.Text,
                Score = entity.Score
            };
            return medEntity;
        }


    }
}
