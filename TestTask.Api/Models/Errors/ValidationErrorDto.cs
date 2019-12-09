namespace TestTask.Api.Models.Errors
{
    /// <summary>
    /// A representation of validation error
    /// </summary>
    public sealed class ValidationErrorDto : ErrorDto
    {
        /// <summary>
        /// A validation summary
        /// </summary>
        public string ValidationSummary { get; set; } = string.Empty;
        /// <summary>
        /// A list of validation errors 
        /// </summary>
        public FieldErrorDto[] ValidationErrors { get; set; }
        public ValidationErrorDto()
        {
            base.Code = ReadableErrorCodes.ValidationError;
        }
    }

    public class FieldErrorDto
    {
        /// <summary>
        /// Field name where error located
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// List of error messages
        /// </summary>
        public string[] ErrorsMessages { get; set; }
        public FieldErrorDto()
        {

        }
        public FieldErrorDto(string field, string[] errorMessages)
        {
            Field = field;
            ErrorsMessages = errorMessages;
        }
    }
}
