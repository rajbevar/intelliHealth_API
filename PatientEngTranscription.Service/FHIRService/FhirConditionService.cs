using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using PatientEngTranscription.Domain.MedicalEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientEngTranscription.Service.FHIRService
{
    public class FhirConditionService
    {

        public async Task<string> AddPatientConditions(string emrPatientId, MedicalConditionRequest MedicalRequestModel
            )
        {
            //List<MedicalConditionEntity> medicalConditionList, List<AnatomyEntity> anatomyList
            if (string.IsNullOrEmpty(emrPatientId))
            {
                throw new Exception("EMR Patient Id is invalid or not found.");
            }

            try
            {

                var request = new Condition
                {
                    ClinicalStatus = Condition.ConditionClinicalStatusCodes.Active,
                    VerificationStatus = Condition.ConditionVerificationStatus.Confirmed,
                    Subject = new ResourceReference
                    {
                        Reference = $"Patient/{emrPatientId}",
                    },
                };

                List<MedicalConditionEntity> medicalConditionList = MedicalRequestModel.MedicalConditions;
                List<AnatomyEntity> anatomyList = MedicalRequestModel.Anatomies;


                request.Category = GetCategories(medicalConditionList);
                request.Severity = GetSeverityList(medicalConditionList);
                request.Code = GetProblems(medicalConditionList);
                request.BodySite = GetBodySites(anatomyList);


                var fhirClient = new FhirClient("http://hapi.fhir.org/baseDstu3");
                var result = await fhirClient.CreateAsync<Condition>(request);

                return result.Id;

            }
            catch (Exception)
            {
                throw;
            }

        }

        private List<CodeableConcept> GetCategories(List<MedicalConditionEntity> medicalConditionList)
        {
            var lst = new List<CodeableConcept>();
            lst.Add(new CodeableConcept
            {
                Coding = new List<Coding>
                 {
                     new Coding
                     {
                          Display="Diagnosis",
                     }
                 }
            });

            return lst;
        }

        private CodeableConcept GetSeverityList(List<MedicalConditionEntity> medicalConditionList)
        {
            return new CodeableConcept
            {
                Coding = new List<Coding>
                 {
                     new Coding
                     {
                          Display="moderate",
                     }
                 }
            };
        }

        private CodeableConcept GetProblems(List<MedicalConditionEntity> medicalConditionList)
        {
            var problem = new CodeableConcept();
            var codingList = new List<Coding>();
            foreach (var item in medicalConditionList)
            {
                var code = new Coding
                {
                    Display = item.Text,
                };

                codingList.Add(code);
            }

            problem.Coding = codingList;
            return problem;
        }

        private List<CodeableConcept> GetBodySites(List<AnatomyEntity> anatomyEntities)
        {

            var bodyList = new List<CodeableConcept>();
            var codingList = new List<Coding>();

            if (anatomyEntities == null) return null;
            foreach (var item in anatomyEntities)
            {

                if (item.Type == "SYSTEM_ORGAN_SITE")
                {
                    var body = new CodeableConcept();
                    codingList.Add(new Coding
                    {
                        Display = item.Text
                    });
                }
            }

            bodyList.Add(new CodeableConcept
            {
                Coding = codingList
            });

            return bodyList;
        }

    }
}
