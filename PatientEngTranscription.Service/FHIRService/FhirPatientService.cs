
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using PatientEngTranscription.Domain;

namespace PatientEngTranscription.Service.FHIRService
{
    public class FhirPatientService
    {

        public async Task<string> AddPatientAsync(PatientCreateRequest patient)
        {
            try
            {               
                var newpa = new Hl7.Fhir.Model.Patient()
                {
                    Active = true,
                    BirthDate = patient.DateOfBirth?.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                   
                Gender = patient.Gender.ToLower() == "female" ? AdministrativeGender.Female : AdministrativeGender.Male,
                    Name = new List<HumanName>()
                {
                    new HumanName()
                    {
                        Use = HumanName.NameUse.Usual,
                        Text=patient.Firstname + patient.Lastname,
                        Family=patient.Lastname,
                        Given = new List<string>() {patient.Firstname }


                    }
                },
                    Telecom = new List<ContactPoint>()
                {
                    new ContactPoint()
                    {
                        Use= ContactPoint.ContactPointUse.Mobile,
                        Rank=1,
                        Value=patient.PhoneNumber
                    }
                }

                };

                var fhirClient = new FhirClient("http://hapi.fhir.org/baseDstu3");
                var result = await fhirClient.CreateAsync<Hl7.Fhir.Model.Patient>(newpa);
                
                return result.Id;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
