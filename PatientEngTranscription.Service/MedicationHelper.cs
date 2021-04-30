using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PatientEngTranscription.Service
{
    public class MedicationHelper
    {

        public static (int, List<String>) GetMedicationFrequentyInNumber(string frequency)
        {
            int intFrequency = 0;
            List<string> frequencyTimings = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(frequency)) return (intFrequency, frequencyTimings);

                var resultDuration = Regex.Match(frequency, @"\d+").Value;
                //if (!string.IsNullOrEmpty(resultDuration))
                //{
                //    var isvalid = int.TryParse(resultDuration, out intFrequency);
                //    return (intFrequency, frequencyTimings);
                //}

                //user regx express: string text = "One car red car blue car";
                //string pat = @"(\w+)\s+(car)";

                bool morning = false;
                frequencyTimings = new List<string>();

                if (frequency.Contains("breakfast", StringComparison.OrdinalIgnoreCase) ||
                    frequency.Contains("before food", StringComparison.OrdinalIgnoreCase) ||
                    frequency.Contains("beforefood", StringComparison.OrdinalIgnoreCase) ||
                    frequency.Contains("break fast", StringComparison.OrdinalIgnoreCase) ||
                    frequency.Contains("morning", StringComparison.OrdinalIgnoreCase) ||
                    frequency.Contains("early morning", StringComparison.OrdinalIgnoreCase) ||
                    frequency.Contains("late morning", StringComparison.OrdinalIgnoreCase))
                {
                    morning = true;
                    frequencyTimings.Add("8 am");
                }

                bool night = false;
                if (frequency.Contains("dinner", StringComparison.OrdinalIgnoreCase) ||
                   frequency.Contains("night", StringComparison.OrdinalIgnoreCase) ||
                   frequency.Contains("pm", StringComparison.OrdinalIgnoreCase))
                {
                    night = true;
                    frequencyTimings.Add("8 pm");
                }

                bool afterNoon=false;
                if (frequency.Contains("afternoon", StringComparison.OrdinalIgnoreCase) ||
                   frequency.Contains("after noon", StringComparison.OrdinalIgnoreCase) ||
                   frequency.Contains("early afternoon", StringComparison.OrdinalIgnoreCase) ||
                   frequency.Contains("late afternoon", StringComparison.OrdinalIgnoreCase) ||
                   frequency.Contains("lunch", StringComparison.OrdinalIgnoreCase))
                {
                    afterNoon = true;
                    frequencyTimings.Add("12 pm");
                }

                bool evening = false;
                if (frequency.Contains("evening", StringComparison.OrdinalIgnoreCase) ||
                   frequency.Contains("early evening", StringComparison.OrdinalIgnoreCase) ||
                   frequency.Contains("late evening", StringComparison.OrdinalIgnoreCase))
                {
                    evening = true;
                    frequencyTimings.Add("5 pm");
                }

                int count = 0;
                count = morning ? count = count + 1 : count;
                count = night ? count = count + 1 : count;
                count = afterNoon ? count = count + 1 : count;
                count = evening ? count = count + 1 : count;

                return (count, frequencyTimings); ;

            }
            catch (Exception)
            {
                return (intFrequency, frequencyTimings);
            }

        }

    }
}
