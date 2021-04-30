using System;

namespace PatientEngTranscription.Domain
{
    public class ResultDto<TContent, TStatus> where TStatus : struct, IComparable, IConvertible, IFormattable
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public TContent Content { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public TStatus Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ErrorDto Error { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultDto{TContent, TStatus}"/> class.
        /// </summary>
        public ResultDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultDto{TContent, TStatus}"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        public ResultDto(TStatus status)
        {
            this.Status = status;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultDto{TContent, TStatus}"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="content">The content.</param>
        public ResultDto(TStatus status, TContent content)
            : this(status)
        {
            this.Content = content;
        }

        public ResultDto(TStatus status, ErrorDto error) : this(status)
        {
            this.Error = error;
        }

    }
}
