using PatientEngTranscription.Domain.Enums;

namespace PatientEngTranscription.Domain
{
    public class ErrorDto
    {
        public ErrorType ErrorType { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public string Details { get; set; }
    }
}
