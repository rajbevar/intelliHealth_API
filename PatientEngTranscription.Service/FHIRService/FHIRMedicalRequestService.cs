using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using PatientEngTranscription.Domain.MedicalEntity;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Hl7.Fhir.Model.MedicationRequest;
using static Hl7.Fhir.Model.Timing;

namespace PatientEngTranscription.Service.FHIRService
{
    public class FhirMedicalRequestService
    {

        public async Task<string> AddMedicinesToPatientAsync(string emrPatientId, List<MedicationEntity> medicineList)
        {
            //   emrPatientId = "1795445";

            if (medicineList == null) return null;

            if(string.IsNullOrEmpty(emrPatientId))
            {
                throw new Exception("EMR Patient Id is invalid or not found.");
            }

            try
            {
                var patientMedicine = new MedicationRequest
                {
                    Status = MedicationRequestStatus.Active,
                    Intent = MedicationRequestIntent.Order,
                    Subject = new ResourceReference
                    {
                        Reference = $"Patient/{emrPatientId}",
                    },
                    Category = new CodeableConcept
                    {
                        Coding = GetMedicineNames(medicineList)
                    },
                    DosageInstruction= GetDosageInstructions(medicineList)
                };


                var fhirClient = new FhirClient("http://hapi.fhir.org/baseDstu3");
                var result = await fhirClient.CreateAsync<MedicationRequest>(patientMedicine);


                return result.Id;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
        public async Task<string> AddMedicinesToPatientAsyncTest()
        {

            try
            {
                var patientMedicine = new MedicationRequest
                {
                    Status = MedicationRequestStatus.Active,
                    Intent = MedicationRequestIntent.Order,
                    Category = new CodeableConcept
                    {
                        Coding = new List<Coding>
                      {
                          new Coding
                          {
                               Code="386864001",
                               Display="amlodipine 5 mg oral tablet"
                          }

                      }
                    },
                    Subject = new ResourceReference
                    {
                        Reference = "Patient/1795445",
                    },
                    DosageInstruction = new List<Dosage>
                {
                    new Dosage
                    {
                         Text="50 mg = 2 tab(s), PO, qDay, # 10 tab(s), 2 Refill(s)",
                           Timing= new Timing
                           {
                                 Repeat= new Timing.RepeatComponent
                                 {
                                      Frequency=1,
                                        Period=1,
                                         PeriodUnit= Timing.UnitsOfTime.D
                                 }
                           },
                           Route= new CodeableConcept
                           {
                                Coding= new List<Coding>
                                {
                                    new Coding
                                    {
                                          Display="Oral"
                                    }
                                }
                           },
                           MaxDosePerLifetime= new Quantity
                           {
                                Value=5,
                                Unit= "mg",
                                System="http://unitsofmeasure.org"
                           }


                    },
                }
                };

                var fhirClient = new FhirClient("http://hapi.fhir.org/baseDstu3");
                var result = await fhirClient.CreateAsync<MedicationRequest>(patientMedicine);

                return result.Id;
            }
            catch (System.Exception ex)
            {
                throw;

            }



        }

        private List<Coding> GetMedicineNames(List<MedicationEntity> medicineList)
        {
            if (medicineList == null) return null;
            var codeList = new List<Coding>();
            foreach (var item in medicineList)
            {
                codeList.Add(new Coding
                {
                    Display = item.Text
                });
            }
            return codeList;
        }

        private List<Dosage> GetDosageInstructions(List<MedicationEntity> medicineList)
        {
            if (medicineList == null) return null;

            var dosageList = new List<Dosage>();
            int i = 1;
            foreach (var item in medicineList)
            {
                var dosage = new Dosage();
                dosage.Sequence = i;
                i++;
                dosage.Text = GetMedicineDosageInstructionText(item);
                dosage.Timing = GetDosageTiming(item);
                dosage.Route = GetRoute(item);
                dosage.MaxDosePerLifetime = GetDosageQuantity(item);

                dosageList.Add(dosage);

            }
            return dosageList;
        }

        private string GetMedicineDosageInstructionText(MedicationEntity entity)
        {
            var display = string.Empty;
            //   dosage.Text = $"{item.Dosage}, {item.RouteOrMode}, {item.Duration}, ";
            //     Text = "50 mg = 2 tab(s), PO, qDay, # 10 tab(s), 2 Refill(s)",
            
            if(!string.IsNullOrEmpty(entity.Dosage))
            {
                display = "," + entity.Dosage;
            }
            if (!string.IsNullOrEmpty(entity.Form))
            {
                display = display + "," + entity.Form;
            }
            if (!string.IsNullOrEmpty(entity.RouteOrMode))
            {
                display = display + "," + entity.RouteOrMode;
            }
            if (!string.IsNullOrEmpty(entity.Frequency))
            {
                display = display + "," + entity.Frequency;
            }
            if (!string.IsNullOrEmpty(entity.Duration))
            {
                display = display + "," + entity.Duration;
            }
            if (!string.IsNullOrEmpty(entity.Rate))
            {
                display = display + "," + entity.Rate;
            }
            if (!string.IsNullOrEmpty(entity.Strength))
            {
                display = display + "," + entity.Strength;
            }
          
            return display;
        }

        private Timing GetDosageTiming(MedicationEntity entity)
        {
            var timing = new Timing();
            timing.Repeat = new Timing.RepeatComponent();

            try
            {
                entity.Duration = entity.Duration == null ? "1" : entity.Duration;
                var resultDuration = Regex.Match(entity.Duration, @"\d+")?.Value;
                if (!string.IsNullOrEmpty(resultDuration))
                {
                    int intDuration;
                    var isvalid = int.TryParse(resultDuration, out intDuration);
                    if (isvalid) timing.Repeat.Duration = intDuration;
                }
                if (entity.Duration.ToLower().Contains("day") || entity.Duration.ToLower().Contains("days"))
                {
                    timing.Repeat.DurationUnit = Timing.UnitsOfTime.D;
                }
                else if (entity.Duration.ToLower().Contains("week") || entity.Duration.ToLower().Contains("weeks"))
                {
                    timing.Repeat.DurationUnit = Timing.UnitsOfTime.Wk;
                }
                else
                {
                    timing.Repeat.DurationUnit = Timing.UnitsOfTime.D;
                }

                if(entity.FrenquencyInDay > 0)
                {
                    timing.Repeat.DurationUnit = Timing.UnitsOfTime.H;
                }

               // timing.Repeat.Count = 1;
                timing.Repeat.Frequency = entity.DurationInNumber;
               
                timing.Repeat.WhenElement = GetFrequencyTimings(entity);
                try
                {
                    if(timing.Repeat.WhenElement!=null)
                    {
                        var intcount = timing.Repeat.WhenElement.Count;
                        timing.Repeat.Period = intcount;
                    }

                }
                catch (Exception)
                {

                    timing.Repeat.Period = 1;
                }
               // timing.EventElement = 
                var objMedicineDuration = new Period();
                objMedicineDuration.StartElement = FhirDateTime.Now();
                objMedicineDuration.EndElement = new FhirDateTime(DateTime.Now.AddDays(entity.DurationInNumber));
                timing.Repeat.Bounds = objMedicineDuration;

                return timing;

            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        private List<Code<EventTiming>> GetFrequencyTimings (MedicationEntity entity)
        {
            try
            {
                var whenList =new  List<Code<EventTiming>>();
                var fhirTimings = new List<FhirDateTime>();

                var (num, timings) = MedicationHelper.GetMedicationFrequentyInNumber(entity.Frequency);
                if (timings == null) return null;

                foreach( var item in timings)
                {
                    if(item.Equals("8 am",StringComparison.OrdinalIgnoreCase))
                    {
                      //  var fhirFrequency = DateTime.Now.AddHours(8);
                      //  fhirTimings.Add(new FhirDateTime(fhirFrequency));

                        whenList.Add(new Code<EventTiming>(EventTiming.MORN));

                    }
                    if (item.Equals("8 pm", StringComparison.OrdinalIgnoreCase))
                    {
                        whenList.Add(new Code<EventTiming>(EventTiming.NIGHT));

                       // var fhirFrequency = DateTime.Now.AddHours(20);
                       // fhirTimings.Add(new FhirDateTime(fhirFrequency));
                    }
                    if (item.Equals("12 pm", StringComparison.OrdinalIgnoreCase))
                    {
                        whenList.Add(new Code<EventTiming>(EventTiming.AFT));
                    }
                    if (item.Equals("5 pm", StringComparison.OrdinalIgnoreCase))
                    {
                        whenList.Add(new Code<EventTiming>(EventTiming.EVE));
                    }
                }
                return whenList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private CodeableConcept GetRoute(MedicationEntity entity)
        {
            CodeableConcept result = new CodeableConcept();
            try
            {
                result.Coding = new List<Coding>();
                result.Coding.Add(new Coding
                {
                    Display = entity.RouteOrMode
                });
                return result;
            }
            catch (System.Exception)
            {

                return null;
            }
        }

        private Quantity GetDosageQuantity(MedicationEntity entity)
        {
            Quantity result = new Quantity();
            try
            {

                var resultDuration = Regex.Match(entity.Dosage, @"\d+").Value;
                if (!string.IsNullOrEmpty(resultDuration))
                {
                    int intDuration;
                    var isvalid = int.TryParse(resultDuration, out intDuration);
                    if (isvalid) result.Value = intDuration;
                }

                if (entity.Dosage.ToLower().Contains("mg") )
                {
                    result.Unit = "mg";
                }
                else if (entity.Dosage.ToLower().Contains("ml"))
                {
                    result.Unit = "ml";
                }
                result.System = "http://unitsofmeasure.org";
                return result;

            }
            catch (System.Exception)
            {

                return result;
            }
        }

    }
}
