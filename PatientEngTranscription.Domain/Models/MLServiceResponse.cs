using Newtonsoft.Json;
using System.Collections.Generic;

namespace PatientEngTranscription.Domain.Models
{
   public  class MLServiceResponse
    {

        public void Error(string errorMessage, string rawErrorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
            RawError = rawErrorMessage;
        }

        public string RawResult { get; set; }
        public Results Results { get; set; }

        public string ErrorMessage { get; set; }

        public string RawError { get; set; }

        public bool IsSuccess { get; set; }

    }

    //public class Rootobject
    //{
    //}

    public class Results
    {
        public List<Output1> output1 { get; set; }
    }

    public class Output1
    {
       
        [JsonProperty(PropertyName = "0")]
        public string value { get; set; }
    }

    //public class OutputValue
    //{
    //    //{"Results":{"output1":[{"0":"Acetaminophen (Tylenol);Ibuprofen;ORS;Ioperamide 150 mg"}]}}
    //    // public object ColumnNames { get; set; }
    //    // public List<string> ColumnNames { get; set; }
    //    //   public string[] ColumnTypes { get; set; }
    //    public string[][] Values { get; set; }
    //}

   


   

}
