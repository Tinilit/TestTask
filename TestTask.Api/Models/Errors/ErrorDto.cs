namespace TestTask.Api.Models.Errors
{
    public class ErrorDto
    {

        /// <summary>
        /// Error code. See documentation fro more details
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Generic error message
        /// </summary>
        public string Message { get; set; }
    }



    public static class ReadableErrorCodes
    {
        public static readonly string ValidationError = "ValidationError";
        public static readonly string ServerFatal = "ServerFatal";
        public static readonly string NotFound = "NotFound";
    }
}
