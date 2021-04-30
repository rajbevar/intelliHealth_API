
namespace PatientEngTranscription.Domain
{
    public class BaseSearchDto
    {
        public const int _DefaultSkip = 0;
        public const int _DefaultTake = 5000;

        public int DefaultPageIndex
        {
            get
            {
                return _DefaultSkip;
            }
        }

        public int DefaultTake
        {
            get
            {
                return _DefaultTake;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSearchDto"/> class.
        /// </summary>
        public BaseSearchDto()
        {
            Skip = _DefaultSkip;
            Take = _DefaultTake;
        }



        /// <summary>
        /// Gets or sets the skip.
        /// </summary>
        /// <value>
        /// The skip.
        /// </value>
        public int? Skip { get; set; }

        /// <summary>
        /// Gets or sets the take.
        /// </summary>
        /// <value>
        /// The take.
        /// </value>
        public int? Take { get; set; }
    }
}
