using AutoWrapper.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientEngTranscription.Api.Helpers
{
    public class ApiResponseException : ApiException
    {

        public int StatusCode { get; set; }
        public bool IsModelValidatonError { get; set; } = false;
        public IEnumerable<ValidationError> Errors { get; set; }
        public string ReferenceErrorCode { get; set; }
        public string ReferenceDocumentLink { get; set; }
        public object CustomError { get; set; }
        public bool IsCustomErrorObject { get; set; } = false;

        public ApiResponseException(object custom, int statusCode = 400) : base(custom, statusCode)
        {
            this.IsCustomErrorObject = true;
            this.StatusCode = statusCode;
            this.CustomError = custom;
        }

        public ApiResponseException(IEnumerable<ValidationError> errors, int statusCode = 400) : base(errors, statusCode)
        {
            this.IsModelValidatonError = true;
            this.StatusCode = statusCode;
            this.Errors = errors;
        }

        public ApiResponseException(Exception ex, int statusCode = 500) : base(ex, statusCode)
        {
            StatusCode = statusCode;
        }

        public ApiResponseException(string message, int statusCode = 400, string errorCode = null, string refLink = null) : base(message, statusCode, errorCode, refLink)
        {
            this.StatusCode = statusCode;
            this.ReferenceErrorCode = errorCode;
            this.ReferenceDocumentLink = refLink;
        }
    }
}
